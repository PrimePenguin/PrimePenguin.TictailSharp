using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailFulfillment : TictailObject
    {
        /// <summary>
        ///     The order ID this fulfillment belongs to.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        ///     The carrier shipping this order. This is a free text field which can be set independently on the actual tracking’s
        ///     carrier (although usually they are the same).
        /// </summary>
        [JsonProperty("shipping_company")]
        public string ShippingCompany { get; set; }

        /// <summary>
        ///     If this order has tracking information, this is the ID for that tracking.
        /// </summary>
        [JsonProperty("tracking_id")]
        public string TrackingId { get; set; }

        /// <summary>
        ///     The carrier’s tracking number for this shipment.
        /// </summary>
        [JsonProperty("tracking_number")]
        public string TrackingNumber { get; set; }

        /// <summary>
        ///     info_received - Package has been registered with the carrier that will ship it.
        ///     in_transit - Package is in transit to its destination.
        ///     out_for_delivery - Package is waiting to be picked up at the destination.
        ///     failed_attempt - A delivery attempt for this package has failed.
        ///     delivered - Package has been delivered successfully to its destination.
        ///     exception - An exception occurred during delivery.
        /// </summary>
        [JsonProperty("tracking_status")]
        public string TrackingStatus { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was created.
        /// </summary>
        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was last modified.
        /// </summary>
        [JsonProperty("modified_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? ModifiedAt { get; set; }
    }
}