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
        ///     See transaction.refund_reason for possible values.
        /// </summary>
        [JsonProperty("refund_reason")]
        public string Reason { get; set; }
    }
}