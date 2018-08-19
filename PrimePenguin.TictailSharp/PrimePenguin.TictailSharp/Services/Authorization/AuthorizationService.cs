using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrimePenguin.TictailSharp.Entities;
using PrimePenguin.TictailSharp.Enums;
using PrimePenguin.TictailSharp.Extensions;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Services.Authorization
{
    public static class AuthorizationService
    {
        private static readonly Regex QuerystringRegex = new Regex(@"[?|&]([\w\.]+)=([^?|^&]+)", RegexOptions.Compiled);

        /// <remarks>
        ///     Source for this method: https://stackoverflow.com/a/22046389
        /// </remarks>
        public static IDictionary<string, string> ParseRawQuerystring(string qs)
        {
            // Must use an absolute uri, else Uri.Query throws an InvalidOperationException
            var uri = new UriBuilder("http://localhost:3000")
            {
                Query = Uri.UnescapeDataString(qs)
            }.Uri;
            var match = QuerystringRegex.Match(uri.PathAndQuery);
            var paramaters = new Dictionary<string, string>();
            while (match.Success)
            {
                paramaters.Add(match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
            }

            return paramaters;
        }

        private static string EncodeQuery(StringValues values, bool isKey)
        {
            var s = values.FirstOrDefault();
            //Important: Replace % before replacing &. Else second replace will replace those %25s.
            if (s == null) return string.Empty;
            var output = s.Replace("%", "%25").Replace("&", "%26");

            if (isKey) output = output.Replace("=", "%3D");
            return output;
        }

        private static string PrepareQuerystring(IEnumerable<KeyValuePair<string, StringValues>> querystring,
            string joinWith)
        {
            var kvps = querystring.Select(kvp => new
            {
                Key = EncodeQuery(kvp.Key, true),
                Value = EncodeQuery(kvp.Value, false)
            })
                .Where(kvp => kvp.Key != "signature" && kvp.Key != "hmac")
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => $"{kvp.Key}={kvp.Value}");

            return string.Join(joinWith, kvps);
        }

        /// <summary>
        ///     Determines if an incoming request is authentic.
        /// </summary>
        /// <param name="querystring">
        ///     The collection of querystring parameters from the request. Hint: use Request.QueryString if
        ///     you're calling this from an ASP.NET MVC controller.
        /// </param>
        /// <param name="tictailSecretKey">Your app's secret key.</param>
        /// <returns>A boolean indicating whether the request is authentic or not.</returns>
        public static bool IsAuthenticRequest(IEnumerable<KeyValuePair<string, StringValues>> querystring,
            string tictailSecretKey)
        {
            var keyValuePairs = querystring.ToList();
            var hmacValues = keyValuePairs.FirstOrDefault(kvp => kvp.Key == "hmac").Value;

            if (string.IsNullOrEmpty(hmacValues) || !hmacValues.Any()) return false;
            var hmac = hmacValues.First();
            var kvps = PrepareQuerystring(keyValuePairs, "&");
            var hmacHasher = new HMACSHA256(Encoding.UTF8.GetBytes(tictailSecretKey));
            var hash = hmacHasher.ComputeHash(Encoding.UTF8.GetBytes(string.Join("&", kvps)));

            //Convert bytes back to string, replacing dashes, to get the final signature.
            var calculatedSignature = BitConverter.ToString(hash).Replace("-", "");

            //Request is valid if the calculated signature matches the signature from the querystring.
            return string.Equals(calculatedSignature, hmac, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        ///     Determines if an incoming request is authentic.
        /// </summary>
        /// <param name="querystring">A dictionary containing the keys and values from the request's querystring.</param>
        /// <param name="tictailSecretKey">Your app's secret key.</param>
        /// <returns>A boolean indicating whether the request is authentic or not.</returns>
        public static bool IsAuthenticRequest(IDictionary<string, string> querystring, string tictailSecretKey)
        {
            var qs = querystring.Select(kvp => new KeyValuePair<string, StringValues>(kvp.Key, kvp.Value));
            return IsAuthenticRequest(qs, tictailSecretKey);
        }

        /// <summary>
        ///     Determines if an incoming request is authentic.
        /// </summary>
        /// <param name="querystring">The request's raw querystring.</param>
        /// <param name="tictailSecretKey">Your app's secret key.</param>
        /// <returns>A boolean indicating whether the request is authentic or not.</returns>
        public static bool IsAuthenticRequest(string querystring, string tictailSecretKey)
        {
            return IsAuthenticRequest(ParseRawQuerystring(querystring), tictailSecretKey);
        }

        /// <summary>
        ///     Determines if an incoming proxy page request is authentic. Conceptually similar to
        ///     <see>
        ///         <cref>IsAuthenticRequest(NameValueCollection, string)</cref>
        ///     </see>
        ///     ,
        ///     except that proxy requests use HMACSHA256 rather than MD5.
        /// </summary>
        /// <param name="querystring">
        ///     The collection of querystring parameters from the request. Hint: use Request.QueryString if
        ///     you're calling this from an ASP.NET MVC controller.
        /// </param>
        /// <param name="tictailSecretKey">Your app's secret key.</param>
        /// <returns>A boolean indicating whether the request is authentic or not.</returns>
        public static bool IsAuthenticProxyRequest(IEnumerable<KeyValuePair<string, StringValues>> querystring,
            string tictailSecretKey)
        {
            // To calculate signature, order all querystring parameters by alphabetical (exclude the
            // signature itself). Then, hash it with the secret key.
            var keyValuePairs = querystring.ToList();
            var signatureValues = keyValuePairs.FirstOrDefault(kvp => kvp.Key == "signature").Value;

            if (string.IsNullOrEmpty(signatureValues) || !signatureValues.Any()) return false;

            var signature = signatureValues.First();
            var kvps = PrepareQuerystring(keyValuePairs, string.Empty);
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(tictailSecretKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(string.Join(null, kvps)));

            //Convert bytes back to string, replacing dashes, to get the final signature.
            var calculatedSignature = BitConverter.ToString(hash).Replace("-", "");

            //Request is valid if the calculated signature matches the signature from the querystring.
            return calculatedSignature.ToUpper() == signature.ToUpper();
        }

        /// <summary>
        ///     Determines if an incoming webhook request is authentic.
        /// </summary>
        /// <param name="requestHeaders">
        ///     The request's headers. Hint: use Request.Headers if you're calling this from an ASP.NET
        ///     MVC controller.
        /// </param>
        /// <param name="inputStream">
        ///     The request's input stream. This method does NOT dispose the stream.
        ///     Hint: use Request.InputStream if you're calling this from an ASP.NET MVC controller.
        /// </param>
        /// <param name="tictailSecretKey">Your app's secret key.</param>
        /// <returns>A boolean indicating whether the webhook is authentic or not.</returns>
        public static async Task<bool> IsAuthenticWebhook(
            IEnumerable<KeyValuePair<string, StringValues>> requestHeaders, Stream inputStream, string tictailSecretKey)
        {
            //Input stream may have already been read when a controller determines parameters to
            //pass to an action. Reset position to 0.
            inputStream.Position = 0;

            //We do not dispose the StreamReader because disposing it will also dispose the input stream,
            //and disposing a request's input stream can cause major headaches for the developer.
            var requestBody = await new StreamReader(inputStream).ReadToEndAsync();

            return IsAuthenticWebhook(requestHeaders, requestBody, tictailSecretKey);
        }

        /// <summary>
        ///     Determines if an incoming webhook request is authentic.
        /// </summary>
        /// <param name="requestHeaders">
        ///     The request's headers. Hint: use Request.Headers if you're calling this from an ASP.NET
        ///     MVC controller.
        /// </param>
        /// <param name="requestBody">The body of the request.</param>
        /// <param name="tictailSecretKey">Your app's secret key.</param>
        /// <returns>A boolean indicating whether the webhook is authentic or not.</returns>
        public static bool IsAuthenticWebhook(IEnumerable<KeyValuePair<string, StringValues>> requestHeaders,
            string requestBody, string tictailSecretKey)
        {
            var hmacHeaderValues = requestHeaders.FirstOrDefault(kvp =>
                kvp.Key.Equals("X-Tictail-Signature", StringComparison.OrdinalIgnoreCase)).Value;

            if (!string.IsNullOrEmpty(hmacHeaderValues) || hmacHeaderValues.Any()) return true;
            return false;

            ////Compute a hash from the apiKey and the request body
            //var hmacHeader = hmacHeaderValues.First();
            //var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("clientsecret_5aaMHcQ6PpsSqcaehrQD2g1HuJoVwfvryCIQUpIx"));
            //var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody)));

            ////Webhook is valid if computed hash matches the header hash
            //return hash == hmacHeader;
        }

        /// <summary>
        ///     A convenience function that tries to ensure that a given URL is a valid tictail domain. It does this by making a
        ///     HEAD request to the given domain, and returns true if the response contains an X-ShopId header.
        ///     **Warning**: a domain could fake the response header, which would cause this method to return true.
        ///     **Warning**: this method of validation is not officially supported by tictail and could break at any time.
        /// </summary>
        /// <param name="url">The URL of the shop to check.</param>
        /// <returns>A boolean indicating whether the URL is valid.</returns>
        public static async Task<bool> IsValidShopDomainAsync(string url)
        {
            var uri = TictailService.BuildShopUri(url, true);

            using (var client = new HttpClient())
            {
                using (var msg = new HttpRequestMessage(HttpMethod.Head, uri))
                {
                    try
                    {
                        var response = await client.SendAsync(msg);

                        return response.Headers.Any(h => h.Key == "X-ShopId");
                    }
                    catch (HttpRequestException)
                    {
                        return false;
                    }
                }
            }
        }

        public static Uri BuildAuthorizationUrl(IEnumerable<TictailAuthorizationScope> scopes, string myShopifyUrl, string shopifyApiKey, string redirectUrl, string state = null)
        {
            return BuildAuthorizationUrl(scopes.Select(s => s.ToSerializedString()), myShopifyUrl, shopifyApiKey, redirectUrl, state);
        }
        /// <summary>
        ///     Builds an authorization URL for tictail OAuth integration.
        /// </summary>
        /// <param name="scopes"></param>
        /// <param name="myTictailUrl">The shop's *.mytictail.com URL.</param>
        /// <param name="tictailApiKey">Your app's public API key.</param>
        /// <param name="redirectUrl"></param>
        /// <param name="state"></param>
        /// <returns>The authorization url.</returns>
        public static Uri BuildAuthorizationUrl(IEnumerable<string> scopes, string myTictailUrl, string tictailApiKey, string redirectUrl, string state = null)
        {
            //Prepare a uri builder for the shop URL
            var builder = new UriBuilder(TictailService.BuildShopUri(myTictailUrl, false));
            var scopeFormateed = $"{string.Join(",", scopes)}";
            //Build the querystring
            var qs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("response_type", "code"),
                new KeyValuePair<string, string>("client_id", tictailApiKey),
                new KeyValuePair<string, string>("scope",scopeFormateed),
                new KeyValuePair<string, string>("state", state),
                new KeyValuePair<string, string>("redirect_uri", HttpUtility.UrlEncode(redirectUrl)),
            };



            builder.Path = "oauth/authorize";
            builder.Query = string.Join("&", qs.Select(s => $"{s.Key}={s.Value}"));

            return builder.Uri;
        }

        /// <summary>
        ///     Authorizes an application installation, generating an access token for the given shop.
        /// </summary>
        /// <param name="authCode"></param>
        /// <param name="myTictailUrl">
        ///     The store's *.tictail.com URL, which should be a paramter named 'shop' on the request
        ///     querystring.
        /// </param>
        /// <param name="tictailApiKey">Your app's public API key.</param>
        /// <param name="tictailSecretKey">Your app's secret key.</param>
        /// <param name="scopes"></param>
        /// <returns>The shop access token.</returns>
        public static async Task<Root> Authorize(string authCode, string myTictailUrl, string tictailApiKey,
            string tictailSecretKey, IEnumerable<TictailAuthorizationScope> scopes)
        {
            return (await AuthorizeWithResult(authCode, myTictailUrl, tictailApiKey, tictailSecretKey, scopes.Select(s => s.ToSerializedString())));
        }

        /// <summary>
        ///     Authorizes an application installation, generating an access token for the given shop.
        /// </summary>
        /// <param name="authCode"></param>
        /// <param name="myTictailUrl">
        ///     The store's *.tictail.com URL, which should be a paramter named 'shop' on the request
        ///     querystring.
        /// </param>
        /// <param name="tictailApiKey">Your app's public API key.</param>
        /// <param name="tictailSecretKey">Your app's secret key.</param>
        /// <param name="scopes"></param>
        /// <returns>The authorization result.</returns>
        public static async Task<Root> AuthorizeWithResult(string authCode, string myTictailUrl,
            string tictailApiKey, string tictailSecretKey, IEnumerable<string> scopes)
        {
            var ub = new UriBuilder(TictailService.BuildShopUri(myTictailUrl, false))
            {
                Path = "oauth/token"
            };

            var values = new Dictionary<string, string>
            {
                { "client_id", $"{tictailApiKey}" },
                { "client_secret", $"{tictailSecretKey}" },
                { "code", $"{authCode}" },
                { "grant_type", "authorization_code"},
                { "scope", $"[{string.Join(",", scopes)}]" },
            };

            var content = new FormUrlEncodedContent(values);

            using (var client = new HttpClient())
            using (var msg = new CloneableRequestMessage(ub.Uri, HttpMethod.Post, content))
            {
                var request = client.SendAsync(msg);
                var response = await request;
                var rawDataString = await response.Content.ReadAsStringAsync();

                TictailService.CheckResponseExceptions(response, rawDataString);

                //var json = JToken.Parse(rawDataString);
                //var a = new AuthorizationResult(json.Value<string>("access_token"),
                //    json.Value<string>("scope").Split(','), json.Value<TictailStore>("store"));
                var json = JsonConvert.DeserializeObject<Root>(rawDataString);
                //return new AuthorizationResult(json.Value<string>("access_token"),
                //    json.Value<string>("scope").Split(','), json.Value<TictailStore>("store"));
                return json;
            }
        }
    }
}