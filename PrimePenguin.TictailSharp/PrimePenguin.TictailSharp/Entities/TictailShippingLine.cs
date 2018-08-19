using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailShippingLine : TictailObject
    {
        /// <summary>
        ///     The order ID this shipping line belongs to.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        ///     Currency in ISO 4217 format for prices in this object.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        ///     Label of the shipping option chosen in checkout.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        ///     Price of this shipping option.
        /// </summary>
        [JsonProperty("price")]
        public decimal? Price { get; set; }

        /// <summary>
        ///     flat - Flat price shipping (i.e. the same cost no matter number of items or destination).
        ///     per_item - Shipping price calculated depending on items and number of items.
        ///     pick_up - Pick up in shop by customer.
        /// </summary>
        [JsonProperty("pricing_type")]
        public string PricingType { get; set; }

        /// <summary>
        ///     ID of the store shipping alternative from which this shipping line is based.
        /// </summary>
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }
    }
}