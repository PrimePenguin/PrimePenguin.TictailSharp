using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailCategory : TictailObject
    {
        /// <summary>
        ///     TThe ID of the parent category, or null if this is a top-level category.
        /// </summary>
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        /// <summary>
        ///     The category title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     The slug of the category, unique within the store.
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        ///     TODO: Include this?
        /// </summary>
        [JsonProperty("position")]
        public decimal? Position { get; set; }

        /// <summary>
        ///     The number of products in this category.
        /// </summary>
        [JsonProperty("product_count")]
        public long? ProductCount { get; set; }

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