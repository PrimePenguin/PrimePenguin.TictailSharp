using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimePenguin.TictailSharp.Infrastructure
{
    public class RequestUri
    {
        private readonly Uri Url;

        public RequestUri(Uri uri)
        {
            Url = uri;
        }

        public Dictionary<string, object> QueryParams { get; } = new Dictionary<string, object>();

        public Uri ToUri()
        {
            // Combine the url and the query param dictionary into a uri
            var query = QueryParams.Select(kvp =>
            {
                return $"{kvp.Key}={Uri.EscapeDataString(kvp.Value.ToString())}";
            });
            var ub = new UriBuilder(Url)
            {
                Query = string.Join("&", query)
            };

            return ub.Uri;
        }

        public override string ToString()
        {
            return ToUri().ToString();
        }
    }
}