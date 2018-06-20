using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class Transaction : TictailObject
    {
        /// <summary>
        ///     The order ID this message belongs to.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        ///     Unguessable generated Tictail transaction ID usually sent to gateways as metadata.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        ///     Currency in ISO 4217 format for prices in this object.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        ///     Transaction amount.
        /// </summary>
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        ///     stripe - The transaction was processed through Stripe.
        ///     paypal - The transaction was processed through PayPal.
        ///     klarna - The transaction was processed through Klarna.
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        ///     The ID of the transaction over at the gateway.
        /// </summary>
        [JsonProperty("gateway_transaction_id")]
        public bool GatewayTransactionId { get; set; }

        /// <summary>
        ///     credit_card - Paid using a credit card (either through Stripe or PayPal).
        ///     stripe - (deprecated) Paid using a credit card through Stripe.Will be replaced with credit_card.
        ///     invoice - Paid using invoice (only for Klarna).
        ///     part_payments - Paid using part payments/installments(only for Klarna).
        ///     paypal - Paid using PayPal account.
        /// </summary>
        [JsonProperty("payment_method")]
        public decimal? PaymentMethod { get; set; }

        /// <summary>
        ///     purhase - Only ever exists one on the order and it is always the first transaction, describing the funds moving
        ///     from customer to merchant.
        ///     refund - Can exist several on the same order, and describes refunds performed by the merchant to the customer.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     succeeded - The transaction is successful and funds have reached its destination.
        ///     pending - The trasaction is currently pending.Depending on the payment service provider it can end up either as
        ///     successful or failed when it settles.
        ///     failed - The transaction is failed (can only happen if the transaction has been pending before). See failure_reason
        ///     for more information.
        ///     error - An unknown error occured when processing this transaction.See error_reason for more information.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        ///     Human-readable reason for why this transaction errored.
        /// </summary>
        [JsonProperty("error_reason")]
        public string ErrorReason { get; set; }

        /// <summary>
        ///     Human-readable reason for why this transaction failed.
        /// </summary>
        [JsonProperty("failure_reason")]
        public string FailureReason { get; set; }

        /// <summary>
        ///     dispute - Because of a dispute.
        ///     item_out_of_stock - Merchant can’t fulfill order because it is out of stock.
        ///     item_not_as_described - Customer says the product isn’t as described, and merchant agrees to a refund.
        ///     item_damage_during_delivery - The item was damaged during delivery.
        ///     item_not_delivered_or_delayed - Item not delivered or delayed delivery.
        ///     shipping_cost - Shipping price was overcharged.
        ///     agreement - Refund according to agreement reached between customer and merchant.
        /// </summary>
        [JsonProperty("refund_reason")]
        public string RefundReason { get; set; }

        /// <summary>
        ///     Human-readable reason for why this transaction is currently pending.
        /// </summary>
        [JsonProperty("pending_reason")]
        public string PendingReason { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was created.
        /// </summary>
        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was last modified.
        /// </summary>
        [JsonProperty("succeeded_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? SucceededAt { get; set; }
    }
}