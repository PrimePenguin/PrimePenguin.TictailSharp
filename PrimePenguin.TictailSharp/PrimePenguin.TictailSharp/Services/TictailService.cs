using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrimePenguin.TictailSharp.Entities;
using PrimePenguin.TictailSharp.Infrastructure;
using PrimePenguin.TictailSharp.Infrastructure.Policies;

namespace PrimePenguin.TictailSharp.Services
{
    public abstract class TictailService
    {
        private static IRequestExecutionPolicy _globalExecutionPolicy = new DefaultRequestExecutionPolicy();

        private static readonly JsonSerializer Serializer =
            new JsonSerializer {DateParseHandling = DateParseHandling.DateTimeOffset};

        private IRequestExecutionPolicy _executionPolicy;

        /// <summary>
        ///     Creates a new instance of <see cref="TictailService" />.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        protected TictailService(string myTictailUrl, string shopAccessToken)
        {
            ShopUri = BuildShopUri(myTictailUrl, false);
            AccessToken = shopAccessToken;

            // If there's a global execution policy it should be set as this instance's policy.
            // User can override it with instance-specific execution policy.
            _executionPolicy = _globalExecutionPolicy ?? new DefaultRequestExecutionPolicy();
        }

        private static HttpClient Client { get; } = new HttpClient();

        protected Uri ShopUri { get; set; }

        protected string AccessToken { get; set; }

        /// <summary>
        ///     Attempts to build a shop API <see cref="Uri" /> for the given shop. Will throw a <see cref="TictailException" /> if
        ///     the URL cannot be formatted.
        /// </summary>
        /// <param name="myTictailUrl">The shop's *.myTictail.com URL.</param>
        /// <param name="withAdminPath"></param>
        /// <exception cref="TictailException">Thrown if the given URL cannot be converted into a well-formed URI.</exception>
        /// <returns>The shop's API <see cref="Uri" />.</returns>
        public static Uri BuildShopUri(string myTictailUrl, bool withAdminPath)
        {
            if (Uri.IsWellFormedUriString(myTictailUrl, UriKind.Absolute) == false)
            {
                //Ticatil typically returns the shop URL without a scheme. If the user is storing that as-is, the uri will not be well formed.
                //Try to fix that by adding a scheme and checking again.
                if (Uri.IsWellFormedUriString("https://" + myTictailUrl, UriKind.Absolute) == false)
                    throw new TictailException(
                        $"The given {nameof(myTictailUrl)} cannot be converted into a well-formed URI.");

                myTictailUrl = "https://" + myTictailUrl;
            }

            var builder = new UriBuilder(myTictailUrl)
            {
                Scheme = "https:",
                Port = 443, //SSL port
                //Path = withAdminPath ? "admin" : ""
            };

            return builder.Uri;
        }

        /// <summary>
        ///     Sets the execution policy for this instance only. This policy will always be used over the global execution policy.
        ///     The instance will revert back to the global execution policy if you pass null to this method.
        /// </summary>
        public void SetExecutionPolicy(IRequestExecutionPolicy executionPolicy)
        {
            // If the user passes null, revert to the global execution policy.
            _executionPolicy = executionPolicy ?? _globalExecutionPolicy ?? new DefaultRequestExecutionPolicy();
        }

        /// <summary>
        ///     Sets the global execution policy for all *new* instances. Current instances are unaffected, but you can call
        ///     .SetExecutionPolicy on them.
        /// </summary>
        public static void SetGlobalExecutionPolicy(IRequestExecutionPolicy globalExecutionPolicy)
        {
            _globalExecutionPolicy = globalExecutionPolicy;
        }

        protected RequestUri PrepareRequest(string path)
        {
            var ub = new UriBuilder(ShopUri)
            {
                Scheme = "https:",
                Port = 443,
                Path = $"/v1.25/{path}"
            };

            return new RequestUri(ub.Uri);
        }

        /// <summary>
        ///     Prepares a request to the path and appends the shop's access token header if applicable.
        /// </summary>
        protected CloneableRequestMessage PrepareRequestMessage(RequestUri uri, HttpMethod method,
            HttpContent content = null)
        {
            var msg = new CloneableRequestMessage(uri.ToUri(), method, content);

            if (!string.IsNullOrEmpty(AccessToken)) msg.Headers.Add("Authorization", $"Bearer {AccessToken}");
            msg.Headers.Add("Accept", "application/json");

            return msg;
        }

        /// <summary>
        ///     Executes a request and returns a JToken for querying. Throws an exception when the response is invalid.
        ///     Use this method when the expected response is a single line or simple object that doesn't warrant its own class.
        /// </summary>
        /// <remarks>
        ///     This method will automatically dispose the
        ///     <paramref>
        ///         <name>baseClient</name>
        ///     </paramref>
        ///     and <paramref name="content" /> when finished.
        /// </remarks>
        protected async Task<JToken> ExecuteRequestAsync(RequestUri uri, HttpMethod method, HttpContent content = null)
        {
            using (var baseRequestMessage = PrepareRequestMessage(uri, method, content))
            {
                var policyResult = await _executionPolicy.Run(baseRequestMessage, async requestMessage =>
                {
                    Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type",
                        "application/json; charset=utf-8");
                    var request = Client.SendAsync(requestMessage);

                    using (var response = await request)
                    {
                        var rawResult = await response.Content.ReadAsStringAsync();

                        //Check for and throw exception when necessary.
                        CheckResponseExceptions(response, rawResult);

                        JToken jtoken = null;

                        // Don't parse the result when the request was Delete.
                        if (baseRequestMessage != null && baseRequestMessage.Method != HttpMethod.Delete)
                            jtoken = JToken.Parse(rawResult);

                        return new RequestResult<JToken>(response, jtoken, rawResult);
                    }
                });

                return policyResult;
            }
        }

        /// <summary>
        ///     Executes a request and returns the given type. Throws an exception when the response is invalid.
        ///     Use this method when the expected response is a single line or simple object that doesn't warrant its own class.
        /// </summary>
        /// <remarks>
        ///     This method will automatically dispose the <paramref>
        ///         <name>baseRequestMessage</name>
        ///     </paramref>
        ///     when finished.
        /// </remarks>
        protected async Task<T> ExecuteRequestAsync<T>(RequestUri uri, HttpMethod method, HttpContent content = null,
            string rootElement = null) where T : new()
        {
            using (var baseRequestMessage = PrepareRequestMessage(uri, method, content))
            {
                var policyResult = await _executionPolicy.Run(baseRequestMessage, async requestMessage =>
                {
                    //Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type",
                    //    "application/json; charset=utf-8");
                    var request = Client.SendAsync(requestMessage);

                    using (var response = await request)
                    {
                        var rawResult = await response.Content.ReadAsStringAsync();

                        //Check for and throw exception when necessary.
                        CheckResponseExceptions(response, rawResult);

                        // This method may fail when the method was Delete, which is intendend.
                        // Delete methods should not be parsing the response JSON and should instead
                        // be using the non-generic ExecuteRequestAsync.
                        var reader = new JsonTextReader(new StringReader(rawResult));
                        if (HttpMethod.Head.Equals(method))
                        {
                            if (response.Headers.Contains("X-Count"))
                            {
                                var data = Convert.ToInt32(response.Headers.GetValues("X-Count").First());
                                return new RequestResult<T>(response, Parse<T>(Convert.ToInt32(data)), rawResult);
                            }
                            else
                            {
                                var data = Convert.ToInt32(response.Headers.GetValues("X-Total-Count").First());
                                return new RequestResult<T>(response, Parse<T>(Convert.ToInt32(data)), rawResult);
                            }
                        }
                        if (rawResult.StartsWith("["))
                        {
                            var data = Serializer.Deserialize<JArray>(reader);
                            var result = data.ToObject<T>();

                            return new RequestResult<T>(response, result, rawResult);
                        }
                        else
                        {
                            var data = Serializer.Deserialize<JObject>(reader);
                            var result = data.ToObject<T>();
                            return new RequestResult<T>(response, result, rawResult);
                        }
                    }
                });

                return policyResult;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static T Parse<T>(int parameter)
        {
          return(T) (object) parameter;
        }
        /// <summary>
        ///     Checks a response for exceptions or invalid status codes. Throws an exception when necessary.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="rawResponse"></param>
        public static void CheckResponseExceptions(HttpResponseMessage response, string rawResponse)
        {
            var statusCode = (int) response.StatusCode;

            // No error if response was between 200 and 300.
            if (statusCode >= 200 && statusCode < 300) return;

            var requestIdHeader =
                response.Headers.FirstOrDefault(h => h.Key.Equals("X-Request-Id", StringComparison.OrdinalIgnoreCase));
            var requestId = requestIdHeader.Value?.FirstOrDefault();
            var code = response.StatusCode;

            var errors = ParseErrorJson(rawResponse);
            var message = $"Response did not indicate success. Status: {(int) code} {response.ReasonPhrase}.";

            if (errors == null)
            {
                errors = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        $"{(int) code} {response.ReasonPhrase}",
                        new[] {message}
                    }
                };
            }
            else
            {
                var firstError = errors.First();

                message = $"{firstError.Key}: {string.Join(", ", firstError.Value)}";
            }

            throw new TictailException(code, errors, message, rawResponse, requestId);
        }

        /// <summary>
        ///     Parses a JSON string for Tictail API errors.
        /// </summary>
        /// <returns>Returns null if the JSON could not be parsed into an error.</returns>
        public static Dictionary<string, IEnumerable<string>> ParseErrorJson(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;

            var errors = new Dictionary<string, IEnumerable<string>>();

            try
            {
                var parsed = JToken.Parse(string.IsNullOrEmpty(json) ? "{}" : json);

                // Errors can be any of the following:
                // 1. { errors: "some error message"}
                // 2. { errors: { "order" : "some error message" } }
                // 3. { errors: { "order" : [ "some error message" ] } }
                // 4. { error: "invalid_request", error_description:"The authorization code was not found or was already used" }

                if (parsed.Any(p => p.Path == "error") && parsed.Any(p => p.Path == "error_description"))
                {
                    // Error is type #4
                    var description = parsed["error_description"];

                    errors.Add("invalid_request", new List<string> {description.Value<string>()});
                }
                else if (parsed.Any(x => x.Path == "errors"))
                {
                    var parsedErrors = parsed["errors"];

                    //errors can be either a single string, or an array of other errors
                    if (parsedErrors.Type == JTokenType.String)
                        errors.Add("Error", new List<string> {parsedErrors.Value<string>()});
                    else
                        foreach (var val in parsedErrors.Values())
                        {
                            var name = val.Path.Split('.').Last();
                            var list = new List<string>();

                            if (val.Type == JTokenType.String)
                                list.Add(val.Value<string>());
                            else if (val.Type == JTokenType.Array) list = val.Values<string>().ToList();

                            errors.Add(name, list);
                        }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                errors.Add(e.Message, new List<string> {json});
            }

            // KVPs are structs and can never be null. Instead, check if the first error equals the default kvp value.
            if (errors.FirstOrDefault().Equals(default(KeyValuePair<string, IEnumerable<string>>))) return null;

            return errors;
        }
    }
}