using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailBriefTerms
    {
        /// <summary>
        ///     The type of terms.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     The version of the terms.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        ///     Dynamic parameters for the terms.
        /// </summary>
        [JsonProperty("parameters")]
        public object Parameters { get; set; }
    }
}