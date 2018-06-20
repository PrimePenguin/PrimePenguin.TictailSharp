using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    /// <summary>
    ///     An entity representing a Tictail product.
    /// </summary>
    public class Product : TictailObject
    {
        /// <summary>
        ///     The ID of the store for this product.
        /// </summary>
        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        /// <summary>
        ///     The Name of the store for this product.
        /// </summary>
        [JsonProperty("store_name")]
        public string StoreName { get; set; }

        /// <summary>
        ///     The subdomain of the store for this product.
        /// </summary>
        [JsonProperty("store_subdomain")]
        public string StoreSubdomain { get; set; }

        /// <summary>
        ///     The URL to the custom shop of the store for this product.
        /// </summary>
        [JsonProperty("store_url")]
        public string StoreUrl { get; set; }

        /// <summary>
        ///     The product title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     The slug of the product, used to build its URL on both the custom shop and the marketplace. Unique within the
        ///     store.
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        ///     The product description. May contain multiple lines and some HTML.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     The product status.
        ///     unpublished - The product is not visible for shoppers and does not show up in search results,
        ///     on the custom shop or the marketplace. It’s still editable by the shop owner.
        ///     published - The product is visible.
        ///     deleted - The product has been deleted.It does not show up in any API responses.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        ///     Currency in ISO 4217 format that the prices this product (and store) are managed in.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        ///     The display price, in cents, for this product.
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        ///     The display original price, in cents, for this product.
        /// </summary>
        [JsonProperty("original_price")]
        public decimal OriginalPrice { get; set; }

        /// <summary>
        ///     The display sale price, in cents, for this product. May be null. Only set to non-null values in non-authorized
        ///     requests if sale_active is true.
        /// </summary>
        [JsonProperty("sale_price")]
        public decimal SalePrice { get; set; }

        /// <summary>
        ///     Display property for whether or not this product has a sale enabled. Only set to true in non-authorized requests if
        ///     sale_active is true.
        /// </summary>
        [JsonProperty("sale_enabled")]
        public bool SaleEnabled { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product goes on sale. Only set to non-null values in
        ///     non-authorized requests if sale_active is true.
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
        ///     Computed display property for whether or not this product is currently on sale. sale_enabled and current datetime
        ///     is in [sale_valid_from, sale_valid_to].
        ///     If either sale_valid_from or sale_valid_to is null, then they are treated as an infinite endpoint which is always
        ///     satisfied by the current datetime.
        /// </summary>
        [JsonProperty("sale_active")]
        public bool SaleActive { get; set; }

        /// <summary>
        ///     The display quantity for this product. Will be null if unlimited is true.
        /// </summary>
        [JsonProperty("quantity")]
        public long? Quantity { get; set; }

        /// <summary>
        ///     Display property for whether or not the quantity of this product is unlimited.
        /// </summary>
        [JsonProperty("unlimited")]
        public string Unlimited { get; set; }

        /// <summary>
        ///     List of variations for this product. All products have at least one variation.
        /// </summary>
        [JsonProperty("variations")]
        public IEnumerable<Variation> Variations { get; set; }

        /// <summary>
        ///     List of images for this product.
        /// </summary>
        [JsonProperty("images")]
        public IEnumerable<ProductImage> Images { get; set; }

        /// <summary>
        ///     List of categories for this product.
        /// </summary>
        [JsonProperty("categories")]
        public IEnumerable<Category> Categories { get; set; }

        /// <summary>
        ///     The ID of the product category this product is categorized as. May be null. See Product Category.
        /// </summary>
        [JsonProperty("discovery_category_id")]
        public string DiscoveryCategoryId { get; set; }


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