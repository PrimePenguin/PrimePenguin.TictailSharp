using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailOrder : TictailObject
    {
        /// <summary>
        ///     Unique integer for the order (always increasing for newer orders).
        /// </summary>
        [JsonProperty("number")]
        public int? Number { get; set; }

        /// <summary>
        ///     Token for this order. This token can be used in place of the ID and then doesn’t require an access token.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        ///     ID of the store the order was placed in.
        /// </summary>
        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        /// <summary>
        ///     Object with basic data about the store the order was placed in, as it were when the order was placed.
        /// </summary>
        [JsonProperty("store")]
        public TictailStore Store { get; set; }

        /// <summary>
        ///     ID of the cart from which this order was created.
        /// </summary>
        [JsonProperty("cart_id")]
        public string CartId { get; set; }

        /// <summary>
        ///     ID of the user that placed this order (not necessarily a user with an account at Tictail).
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        ///     Whether the user was signed in to a Tictail account when the order was placed.
        /// </summary>
        [JsonProperty("user_signed_in")]
        public bool UserSignedIn { get; set; }

        /// <summary>
        ///     Email of the customer placing this order (as entered in checkout).
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        ///     open - Order is open. The order will remain in this status as long as it has not been actively cancelled.
        ///     cancelled - Order has been cancelled (this doesn’t necessarily mean that payment has been refunded).
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        //  paid - The order is fully paid.
        //  refunded_partially - Part of the payment has been refunded (see transactions to see amounts).
        //  refunded_fully - The payment of this order has been fully refunded (this will also be the final status if the order is partially refunded up to its full amount).
        /// <summary>
        ///     Status for the payment of this order. For more details, inspect transactions.
        ///     unpaid - Order not yet paid (a completed order will never have this status).
        ///     pending - Payment is pending, meaning that the payment gateway needs to confirm it before the order is considered
        ///     paid.
        ///     The payment can however also be denied, in which case the order should not be shipped.
        /// </summary>
        [JsonProperty("payment_status")]
        public string PaymentStatus { get; set; }

        /// <summary>
        ///     undisputed - Order has not been disputed.
        ///     eligible - Order is eligible for dispute(10 days has passed since the order was placed, 60 days haven’t passed, and
        ///     the order is either paid or partially_refunded).
        ///     ineligible - Order is not eligible for dispute.
        ///     reported - Order has been reported by customer, but has not yet escalated into a dispute.
        ///     awaiting_evidence - Order has escalated to a dispute, and the dispute is now awaiting evidence from the merchant to
        ///     defend it.
        ///     awaiting_resolution - Dispute is awaiting resolution, either from Tictail or from external gateway(such as a bank).
        ///     resolved - Dispute has been resolved, either in favor of customer or in favor of merchant.
        /// </summary>
        [JsonProperty("dispute_status")]
        public string DisputeStatus { get; set; }

        /// <summary>
        ///     unfulfilled - Order has not yet been shipped.
        ///     fulfilled - Order has been shipped.
        /// </summary>
        [JsonProperty("fulfillment_status")]
        public string FulfillmentStatus { get; set; }

        /// <summary>
        ///     cancelled - Order has been cancelled.
        ///     pending - Order’s payment is pending.
        ///     awaiting_fulfillment - Order is waiting to be shipped.
        ///     awaiting_pick_up - Order is waiting to be picked up.
        ///     refunded_partially - Order has been partially refunded.
        ///     refunded_fully - Order has been fully refunded.
        ///     shipped - Order has been shipped.
        ///     picked_up - Order has been picked up.
        ///     denied - Order’s payment was previously pending but has now settled as denied.
        ///     info_received - Order has been registered with the carrier that will ship it.
        ///     in_transit - Order is in transit to its destination.
        ///     out_for_delivery - Order is waiting to be picked up at the destination.
        ///     failed_attempt - A delivery attempt for this order has failed.
        ///     delivered - Order has been delivered successfully to its destination.
        ///     exception - An exception occurred during delivery.
        /// </summary>
        [JsonProperty("display_status")]
        public string DisplayStatus { get; set; }

        /// <summary>
        ///     Currency in ISO 4217 format the customer was charged in.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        ///     (expandable) Object describing the address to which this order should be shipped.
        /// </summary>
        [JsonProperty("shipping_address")]
        public TictailAddress ShippingAddress { get; set; }

        /// <summary>
        ///     (expandable) Object describing the shipping details of this order. This object always exists, and represents the
        ///     choices made by the customer in checkout.
        /// </summary>
        [JsonProperty("shipping_line")]
        public TictailShippingLine ShippingLine { get; set; }

        /// <summary>
        ///     (expandable) Object describing the shipment information for the order. Note that this object only exists if the
        ///     order has been shipped.
        /// </summary>
        [JsonProperty("fulfillment")]
        public TictailFulfillment Fulfillment { get; set; }

        /// <summary>
        ///     (expandable) List of messages belonging to this order. Currently, this only ever contains one message - the message
        ///     that was sent to the customer when the order was marked as shipped. If no message was sent, this will be an empty
        ///     list.
        /// </summary>
        [JsonProperty("messages")]
        public IEnumerable<TictailMessage> Messages { get; set; }

        /// <summary>
        ///     (expandable)
        ///     List of messages belonging to this order. Currently, this only ever contains one message - the message that was
        ///     sent to the customer when the order was marked as shipped. If no message was sent, this will be an empty listList
        ///     of items belonging to this order. Each item corresponds to a purchased product variation.
        /// </summary>
        [JsonProperty("items")]
        public IEnumerable<TictailItem> Items { get; set; }

        /// <summary>
        ///     (expandable)  List of adjustments for this order. This list will always contain at least shipping-tax and item-tax
        ///     (even if included/set to zero).
        /// </summary>
        [JsonProperty("adjustments")]
        public IEnumerable<TictailAdjustment> Adjustments { get; set; }

        /// <summary>
        ///     (expandable) List of transactions for this order. The first item is always the purchase transaction on the full
        ///     grand total of the order. Other transactions, if existing, are refund transactions refunding part of or the full
        ///     grand total amount.
        /// </summary>
        [JsonProperty("transactions")]
        public IEnumerable<TictailTransaction> Transactions { get; set; }

        /// <summary>
        ///     (expandable) List of disputes for this order. A dispute can either be created at Tictail by customers, or come from
        ///     gateways (such as PayPal disputes or Stripe credit card chargebacks).
        /// </summary>
        [JsonProperty("disputes")]
        public IEnumerable<TictailDispute> Disputes { get; set; }

        /// <summary>
        ///     (expandable) List of internal notes for this order. These can only be read be the merchant (so if scope
        ///     user.order.read is used to read the order this field won’t be included).
        ///     They are intended for reminders/remarks by the merchant, and are never shared with the customer in any way
        /// </summary>
        [JsonProperty("internal_notes")]
        public IEnumerable<TictailInternalNote> InternalNotes { get; set; }

        /// <summary>
        ///     Specifies from where this order came, on the form source:platform.
        /// </summary>
        [JsonProperty("attribution")]
        public string Attribution { get; set; }

        /// <summary>
        ///     The locale used by the customer in checkout. All communication regarding this order will be done in this locale.
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        /// <summary>
        ///     Message written by the customer for this order.
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        ///     Whether the prices listed in this order includes tax or not.
        /// </summary>
        [JsonProperty("prices_include_tax")]
        public string PricesIncludeTax { get; set; }

        /// <summary>
        ///     Type of tax the country where the store is in uses.
        /// </summary>
        [JsonProperty("tax_type")]
        public string TaxType { get; set; }

        /// <summary>
        ///     Denotes whether this sale was driven by Tictail.
        /// </summary>
        [JsonProperty("tictail_shopper")]
        public bool TictailShopper { get; set; }

        /// <summary>
        ///     Total amount of discounts applied to this order. For more details, inspect adjustments.
        /// </summary>
        [JsonProperty("discount_total")]
        public decimal? DiscountTotal { get; set; }

        /// <summary>
        ///     Total amount of this order. This is what the customer paid at checkout, and it does not change with refunds.
        /// </summary>
        [JsonProperty("grand_total")]
        public decimal? GrandTotal { get; set; }

        /// <summary>
        ///     The item total minus any discounts applied to the order.
        /// </summary>
        [JsonProperty("subtotal")]
        public decimal? Subtotal { get; set; }

        /// <summary>
        ///     Invoice fee paid for this order. Only applicable for some payment methods.
        /// </summary>
        [JsonProperty("invoice_fee")]
        public decimal? InvoiceFee { get; set; }

        /// <summary>
        ///     Tax on the invoice fee.ax on the invoice fee.
        /// </summary>
        [JsonProperty("invoice_fee_tax")]
        public decimal? InvoiceFeeTax { get; set; }

        /// <summary>
        ///     Total amount of the items (products) on this order.
        /// </summary>
        [JsonProperty("item_total")]
        public decimal? ItemTotal { get; set; }

        /// <summary>
        ///     The tax amount on the items of this order.
        /// </summary>
        [JsonProperty("item_tax")]
        public decimal? ItemTax { get; set; }

        /// <summary>
        ///     Amount available for refund. This decreases with refunds made on the order.
        /// </summary>
        [JsonProperty("refundable_total")]
        public decimal? RefundableTotal { get; set; }

        /// <summary>
        ///     Tax amount paid on shipping for this order.
        /// </summary>
        [JsonProperty("shipping_total")]
        public decimal? ShippingTotal { get; set; }

        /// <summary>
        ///     Tax amount paid on shipping for this order.
        /// </summary>
        [JsonProperty("shipping_tax")]
        public decimal? ShippingTax { get; set; }

        /// <summary>
        ///     The total amount of tax on this order.
        /// </summary>
        [JsonProperty("tax_total")]
        public decimal? TaxTotal { get; set; }

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