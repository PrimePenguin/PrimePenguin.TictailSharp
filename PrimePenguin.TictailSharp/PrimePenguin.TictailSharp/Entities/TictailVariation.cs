using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailVariation : TictailObject
    {
        /// <summary>
        ///     The SKU (Stock Keeping Unit) of this product. May be null. Is not guaranteed to be unique within the store.
        /// </summary>
        [JsonProperty("sku")]
        public string Sku { get; set; }

        /// <summary>
        ///     The variation title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     The display price, in cents, for this variation. Will be either original_price or sale_price, depending on
        ///     sale_active
        /// </summary>
        [JsonProperty("price")]
        public decimal? Price { get; set; }

        /// <summary>
        ///     The original price, in cents, for this variation.
        /// </summary>
        [JsonProperty("original_price", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public decimal? OriginalPrice { get; set; }

        /// <summary>
        ///     The sale price, in cents, for this product. May be null. Only set to non-null values in non-authorized requests if
        ///     sale_active is true.
        /// </summary>
        [JsonProperty("sale_price")]
        public decimal? SalePrice { get; set; }

        /// <summary>
        ///     Display property for whether or not this product has a sale enabled. Only set to true in non-authorized requests if
        ///     sale_active is true.
        /// </summary>
        [JsonProperty("sale_enabled")]
        public bool SaleEnabled { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product goes on sale.
        ///     Only set to non-null values in non-authorized requests if sale_active is true.
        /// </summary>
        [JsonProperty("sale_valid_from")]
        public DateTimeOffset? SaleValidFrom { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product goes off sale. Only set to non-null values in
        ///     non-authorized requests if sale_active is true.
        /// </summary>
        [JsonProperty("sale_valid_to")]
        public DateTimeOffset? SaleValidTo { get; set; }

        /// <summary>
        ///     SComputed display property for whether or not this product is currently on sale. sale_enabled and current datetime
        ///     is in [sale_valid_from, sale_valid_to].
        ///     If either sale_valid_from or sale_valid_to is null,  then they are treated as an infinite endpoint which is always
        ///     satisfied by the current datetime.
        /// </summary>
        [JsonProperty("sale_active")]
        public bool SaleActive { get; set; }

        /// <summary>
        ///     The quantity for this variation. Will be null if unlimited is true.
        /// </summary>
        [JsonProperty("quantity")]
        public long? Quantity { get; set; }

        /// <summary>
        ///     The unique identifier for the inventory item, which is used in the Inventory API to query for inventory
        ///     information.
        /// </summary>
        [JsonProperty("inventory_item_id")]
        public long? InventoryItemId { get; set; }


        /// <summary>
        ///     The date and time when the product variant was created. The API returns this value in ISO 8601 format.
        /// </summary>
        [JsonProperty("unlimited")]
        public bool Unlimited { get; set; }

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