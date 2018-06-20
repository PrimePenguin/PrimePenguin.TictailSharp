using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class InternalNote : TictailObject
    {
        /// <summary>
        ///     The order ID this message belongs to.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        ///     Text written by merchant in the note.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        ///     User ID of the shop owner that wrote the note.
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was created.
        /// </summary>
        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }
    }
}