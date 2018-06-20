using Newtonsoft.Json;
using PrimePenguin.TictailSharp.Services.Product;

namespace PrimePenguin.TictailSharp.Filters
{
    /// <summary>
    ///     <see cref="ProductService.ListAsync(string,ProductFilter)" /> results.
    /// </summary>
    public class ProductFilter : PublishableListFilter
    {
        /// <summary>
        ///     Filter by product title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Filter by product store_name.
        /// </summary>
        [JsonProperty("store_name")]
        public string StoreName { get; set; }
    }
}