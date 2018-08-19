using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    /// <summary>
    ///     An object representing a Tictail event.
    /// </summary>
    public class TictailEvent
    {
        /// <summary>
        ///     The ID of this event. Unique for all of Tictail.
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        /// <summary>
        ///     Name and type of event.
        /// </summary>
        [JsonProperty("event_name")]
        public string EventName { get; set; }

        /// <summary>
        ///     When this event was initially created and sent.
        /// </summary>
        [JsonProperty("event_at")]
        public string EventAt { get; set; }

        /// <summary>
        ///     Current delivery status, can be one of pending, sent, and failed.
        /// </summary>
        [JsonProperty("event_status")]
        public string EventStatus { get; set; }

        /// <summary>
        ///     Type of object, can be one of order and product.
        /// </summary>
        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        /// <summary>
        ///     ID of the affected order or product.
        /// </summary>
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        /// <summary>
        ///     ID of the affected store.
        /// </summary>
        [JsonProperty("store_id")]
        public string StoreId { get; set; }
    }
}