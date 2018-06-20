using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class Item : TictailObject
    {
        /// <summary>
        ///     The order ID this item belongs to.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        ///     URL to an image of the product.
        /// </summary>
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        ///     Currency in ISO 4217 format for prices in this object.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        ///     Unit price for this product variation.
        /// </summary>
        [JsonProperty("price")]
        public decimal? Price { get; set; }

        /// <summary>
        ///     Quantity purchased.
        /// </summary>
        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        /// <summary>
        ///     Total price for this item (price * quantity).
        /// </summary>
        [JsonProperty("total")]
        public decimal? Total { get; set; }

        /// <summary>
        ///     ID of the product this item was created from.
        /// </summary>
        [JsonProperty("product_id")]
        public long ProductId { get; set; }


        /// <summary>
        ///     URL slug for the product.
        /// </summary>
        [JsonProperty("product_slug")]
        public string ProductSlug { get; set; }

        /// <summary>
        ///     ID of the variation this item was created from.
        /// </summary>
        [JsonProperty("variation_id")]
        public string VariationId { get; set; }

        /// <summary>
        ///     The product title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     The variation title.
        /// </summary>
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        /// <summary>
        ///     Stock keeping unit for this product.
        /// </summary>
        [JsonProperty("sku")]
        public string Sku { get; set; }
    }
}