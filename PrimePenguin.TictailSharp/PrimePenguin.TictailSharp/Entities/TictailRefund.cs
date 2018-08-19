using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailRefund : TictailObject
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
       
        [JsonProperty("currency")]
        public string Currency { get; set; }
        
        [JsonProperty("amount")]
        public decimal? OriginalPrice { get; set; }

        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("gateway_transaction_id")]
        public string GatewayTransactionId { get; set; }

        [JsonProperty("payment_method")]
        public string PaymentMethod { get; set; }
       
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("failure_reason")]
        public string FailureReason { get; set; }

        [JsonProperty("error_reason")]
        public string ErrorReason { get; set; }

        [JsonProperty("refund_reason")]
        public string RefundReason { get; set; }

        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("succeeded_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? SucceededAt { get; set; }
    }
}