using Newtonsoft.Json;
using PrimePenguin.TictailSharp.Services.Order;

namespace PrimePenguin.TictailSharp.Filters
{
    /// <summary>
    ///     Options for filtering <see cref="OrderService.CountAsync" /> and
    ///     <see cref="OrderService.ListAsync(string,string,OrderFilter)" /> results.
    /// </summary>
    public class OrderFilter : ListFilter
    {
        /// <summary>
        ///     ID of the store to query for (required if user_id is not provided).
        /// </summary>
        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        /// <summary>
        ///     ID of the user to query for (required if store_id is not provided).
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}