using System;
using System.Collections.Generic;
using System.Net;

namespace PrimePenguin.TictailSharp.Infrastructure
{
    public class TictailException : Exception
    {
        public TictailException()
        {
        }

        public TictailException(string message) : base(message)
        {
        }

        public TictailException(HttpStatusCode httpStatusCode, Dictionary<string, IEnumerable<string>> errors,
            string message, string rawBody, string requestId) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
            RawBody = rawBody;
            RequestId = requestId;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        ///     The XRequestId header returned by Tictail. Can be used when working with the Ticatil support team to identify the
        ///     failed request.
        /// </summary>
        public string RequestId { get; set; }

        /// <remarks>
        ///     Dictionary is always initialized to ensure null reference errors won't be thrown when trying to check error
        ///     messages.
        /// </remarks>
        public Dictionary<string, IEnumerable<string>> Errors { get; set; } =
            new Dictionary<string, IEnumerable<string>>();

        /// <summary>
        ///     The raw JSON string returned by Tictail.
        /// </summary>
        public string RawBody { get; set; }
    }
}