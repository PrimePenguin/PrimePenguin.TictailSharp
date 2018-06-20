using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Services.Order
{
    public class OrderCancelOptions
    {
        /// <summary>
        ///     The amount to refund. If not specified, the full (or remaining) amount will be refunded.
        /// </summary>
        [JsonProperty("amount")]
        public decimal? RefundAmount { get; set; }

        /// <summary>
        ///     Currency in ISO 4217 format for amount. Must match the order.currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        ///     See transaction.refund_reason for possible values.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}