using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailDispute : TictailObject
    {
        /// <summary>
        ///     The order ID this message belongs to.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        ///     Transaction ID of the transaction being disputed.
        /// </summary>
        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        /// <summary>
        ///     reported - This dispute is currently only a report. It might escalate into a dispute later on, but at this stage it
        ///     could be resolved directly between customer and merchant.
        ///     awaiting_evidence - The report has escalated to a dispute and is now awaiting evidence from the merchant to defent
        ///     the dispute.Disputes received directly from gateways usually start in this state.
        ///     awaiting_resolution - Evidence has been submitted (or evidence was not possible to submit) and the dispute is now
        ///     awaiting a resolution.
        ///     favor_merchant - The dispute has been decided in favor of the merchant.
        ///     favor_consumer - The dispute has been decided in favor of the customer.
        ///     overridden - The dispute has been overriden by another dispute. This can happen if the order was first reported
        ///     through Tictail, but also chargebacked through the customer’s bank/at PayPal. In this case, the gateway dispute
        ///     overrides Tictail’s dispute (and the order will have two disputes in the disputes array).
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        ///     unknown - The reason for the dispute is unknown.
        ///     fraudulent - Someone unauthorized placed this order without the consent of the customer (for example using a stolen
        ///     credit card).
        ///     order_not_received - The customer indicated they did not receive the order.
        ///     item_not_as_described - The customer indicated the item(s) they received were not as described.
        /// </summary>
        [JsonProperty("reason_code")]
        public string ReasonCode { get; set; }

        /// <summary>
        ///     Message provided by the customer when the dispute was created. Only available for PayPal disputes.
        /// </summary>
        [JsonProperty("customer_message")]
        public string CustomerMessage { get; set; }

        /// <summary>
        ///     The payment gateway this dispute is from. This field is null if the dispute was filed at Tictail.
        ///     stripe - The dispute was created through Stripe(usually through a chargeback from the customer’s bank).
        ///     paypal - The dispute was created in PayPal’s dispute resolution center.
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        ///     ID identifying the dispute over at the gateway.
        /// </summary>
        [JsonProperty("gateway_dispute_id")]
        public string GatewayDisputeId { get; set; }

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