using System.Collections.Generic;
using System.Net;

namespace PrimePenguin.TictailSharp.Infrastructure
{
    /// <summary>
    ///     An exception thrown when an API call has reached Tictail's rate limit.
    /// </summary>
    public class TictailRateLimitException : TictailException
    {
        public TictailRateLimitException()
        {
        }

        public TictailRateLimitException(string message) : base(message)
        {
        }

        public TictailRateLimitException(HttpStatusCode httpStatusCode, Dictionary<string, IEnumerable<string>> errors,
            string message, string jsonError, string requestId) : base(httpStatusCode, errors, message, jsonError,
            requestId)
        {
        }
    }
}