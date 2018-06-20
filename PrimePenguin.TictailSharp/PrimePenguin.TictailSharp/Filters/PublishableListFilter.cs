using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Filters
{
    /// <summary>
    ///     Generic options for filtering objects that can be published (e.g. those with a PublishedAt, Published status).
    /// </summary>
    public class PublishableListFilter : ListFilter
    {
        /// <summary>
        ///     Show objects modified_at after date (format: 2008-12-31 03:00).
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        ///     Show objects modified_at before date (format: 2008-12-31 03:00).
        /// </summary>
        [JsonProperty("modified_at")]
        public DateTimeOffset? ModifiedAt { get; set; }

        /// <summary>
        ///     Published Status.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}