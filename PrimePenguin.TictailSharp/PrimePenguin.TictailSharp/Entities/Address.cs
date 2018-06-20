using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class Address : TictailObject
    {
        /// <summary>
        ///     First and last name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Address line 1.
        /// </summary>
        [JsonProperty("line1")]
        public string Line1 { get; set; }

        /// <summary>
        ///     Address line 2.
        /// </summary>
        [JsonProperty("line2")]
        public string Line2 { get; set; }

        /// <summary>
        ///     ZIP/postal code.
        /// </summary>
        [JsonProperty("zip")]
        public string Zip { get; set; }

        /// <summary>
        ///     The city.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        ///     State. For addresses with country set to CA, US, ES, and PT this is a state code in ISO 3166-2 format.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        ///     Country in ISO 3166-1 Alpha-2 code format.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        ///     Phone number, no formatting applied.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        ///     checkout - Address entered at checkout.
        ///     account - Address from customers’s shipping address on their Tictail account.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }
    }
}