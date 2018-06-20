using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class Carrier : TictailObject
    {
        /// <summary>
        ///     TThe ID of the parent category, or null if this is a top-level category.
        /// </summary>
        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        /// <summary>
        ///     The name of the carrier.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}