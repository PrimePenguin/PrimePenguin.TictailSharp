using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class BriefTerms
    {
        /// <summary>
        ///     The type of terms.
        /// </summary>
        [JsonProperty("type")]
        public bool Type { get; set; }

        /// <summary>
        ///     The version of the terms.
        /// </summary>
        [JsonProperty("version")]
        public bool Version { get; set; }

        /// <summary>
        ///     Dynamic parameters for the terms.
        /// </summary>
        [JsonProperty("parameters")]
        public object Parameters { get; set; }
    }
}