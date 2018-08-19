using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailCustomer : TictailObject
    {
        /// <summary>
        ///     Email to the customer. Note that this can be a proxy email address.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        ///     Full name of the customer.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Country of the customer (in ISO 3166-1 Alpha-2 code format).
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        ///     Preferred language of the customer (in ISO 639-1 format).
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was created.
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was last modified.
        /// </summary>
        [JsonProperty("modified_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ModifiedAt { get; set; }
    }
}