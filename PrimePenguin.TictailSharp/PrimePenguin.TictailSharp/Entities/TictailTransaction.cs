using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailAdjustment : TictailObject
    {
        /// <summary>
        ///     The order ID this message belongs to.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        ///     item-tax - Adjustment representing item tax on this order.
        ///     shipping-tax - Adjustment representing shipping tax on this order.
        ///     Invoice-fee - Adjustment representing an invoice fee applied to this order (used with some Klarna orders).
        ///     invoice-fee-tax - Adjustment representing the invoice fee tax of the invoice fee.
        ///     discount - Adjustment representing a discount applied to this order. title will have the discount title, and data
        ///     will contain the promo code that was used.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Currency in ISO 4217 format for prices in this object.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        ///     Whether this adjustment is already included in prices (for example, the item price might already include the item
        ///     tax).
        /// </summary>
        [JsonProperty("included")]
        public bool Included { get; set; }

        /// <summary>
        ///     Whether this adjustment is a tax adjustment.
        /// </summary>
        [JsonProperty("is_tax")]
        public bool IsTax { get; set; }

        /// <summary>
        ///     The price of this adjustment.
        /// </summary>
        [JsonProperty("price")]
        public decimal? Price { get; set; }

        /// <summary>
        ///     Title of the adjustment. Used for discount title, for example.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     ID of an object this adjustment was created from/because of.
        /// </summary>
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        /// <summary>
        ///     Type of object reference_id is referring to.
        /// </summary>
        [JsonProperty("reference_type")]
        public string ReferenceType { get; set; }
    }
}