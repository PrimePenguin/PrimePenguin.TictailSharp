using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailVat
    {
        /// <summary>
        ///     Whether or not the tax is included in the product prices.
        /// </summary>
        [JsonProperty("included_in_prices")]
        public bool IncludedInPrices { get; set; }

        /// <summary>
        ///     Whether or not the tax is applied to shipping prices.
        /// </summary>
        [JsonProperty("applied_to_shipping")]
        public bool AppliedToShipping { get; set; }

        /// <summary>
        ///     Whether or not automatic tax calculation is enabled.
        /// </summary>
        [JsonProperty("automatic_calculation")]
        public bool AutomaticCalculation { get; set; }

        /// <summary>
        ///     The country or region for which tax should be applied.
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }

        /// <summary>
        ///     The tax rate, as a string decimal. Min: 0.00, max: 1.00.
        /// </summary>
        [JsonProperty("rate")]
        public string Rate { get; set; }

        /// <summary>
        ///     The VAT number for this store.
        /// </summary>
        [JsonProperty("vat_number")]
        public string VatNumber { get; set; }
    }
}