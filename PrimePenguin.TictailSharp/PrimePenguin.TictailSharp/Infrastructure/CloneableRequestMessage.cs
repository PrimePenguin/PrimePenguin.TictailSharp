using System;
using System.Net.Http;

namespace PrimePenguin.TictailSharp.Infrastructure
{
    public class CloneableRequestMessage : HttpRequestMessage
    {
        public CloneableRequestMessage(Uri url, HttpMethod method, HttpContent content = null) : base(method, url)
        {
            if (content != null) Content = content;
        }

        public CloneableRequestMessage Clone()
        {
            var newContent = Content;

            if (newContent != null && newContent is JsonContent c) newContent = c.Clone();

            var cloned = new CloneableRequestMessage(RequestUri, Method, newContent);

            // Copy over the request's headers which includes the access token if set
            foreach (var header in Headers) cloned.Headers.Add(header.Key, header.Value);

            return cloned;
        }
    }
}