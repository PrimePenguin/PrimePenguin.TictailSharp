using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class Message : TictailObject
    {
        /// <summary>
        ///     The order ID this message belongs to.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        ///     The text of the message sent to the customer.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        ///     The user ID of the shop owner who sent this message (or the first shop owner of the store if a third-party app sent
        ///     the message).
        /// </summary>
        [JsonProperty("author_id")]
        public string AuthorId { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was created.
        /// </summary>
        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }
    }
}