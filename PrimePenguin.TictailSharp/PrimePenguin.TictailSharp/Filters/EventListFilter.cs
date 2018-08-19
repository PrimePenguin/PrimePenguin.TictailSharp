using Newtonsoft.Json;
using PrimePenguin.TictailSharp.Infrastructure;

namespace PrimePenguin.TictailSharp.Filters
{
    /// <summary>
    ///     A generic class for filtering the results of a .CountAsync command.
    /// </summary>
    public class EventListFilter : Parameterizable
    {
        /// <summary>
        ///     Limit the amount of results. Default is 50, max is 100.
        /// </summary>
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        /// <summary>
        ///     Page of results to be returned. Default is 1.
        /// </summary>
        [JsonProperty("page")]
        public int? Page { get; set; }

        /// <summary>
        ///     Restrict results to after the specified ID
        /// </summary>
        [JsonProperty("since_id")]
        public long? SinceId { get; set; }

        /// <summary>
        ///     Only show events specified in filter (comma , separated).
        /// </summary>
        [JsonProperty("filter")]
        public string Filters { get; set; }
    }
}