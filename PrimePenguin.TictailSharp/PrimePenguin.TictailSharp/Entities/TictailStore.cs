using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailStore : TictailObject
    {
        /// <summary>
        ///     Unique but mutable subdomain (x.tictail.com) for the store.
        /// </summary>
        [JsonProperty("subdomain")]
        public string Subdomain { get; set; }

        /// <summary>
        ///     Name of the store.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     URL to the custom shop for this store.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        ///     Whether or not this store is online or not.
        /// </summary>
        [JsonProperty("online")]
        public bool Online { get; set; }

        /// <summary>
        ///     Whether or not this store is visible in the Tictail marketplace.
        /// </summary>
        [JsonProperty("visible_in_marketplace")]
        public bool VisibleInMarketplace { get; set; }

        /// <summary>
        ///     Currency in ISO 4217 format that the prices this store (and products) are managed in.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        ///     Language of the store’s custom shop (in ISO 639-1 format).
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        ///     Store’s country in ISO 3166-1 Alpha-2 code format.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        ///     State of the store, only for the US.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        ///     Short description of the store.
        /// </summary>
        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        /// <summary>
        ///     Description of the store.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     URL to external blog.
        /// </summary>
        [JsonProperty("blog_url")]
        public string BlogUrl { get; set; }

        /// <summary>
        ///     Number of followers.
        /// </summary>
        [JsonProperty("followers")]
        public long Followers { get; set; }

        /// <summary>
        ///     VAT settings of the store, see VAT object for details.
        /// </summary>
        [JsonProperty("vat")]
        public TictailVat Vat { get; set; }

        /// <summary>
        ///     List with the logotype.
        /// </summary>
        [JsonProperty("logotype")]
        public IEnumerable<TictailLogoType> Logotype { get; set; }

        /// <summary>
        ///     Map of wallpapers by their categories.
        /// </summary>
        [JsonProperty("wallpapers")]
        public TictailWallpaper Wallpapers { get; set; }

        /// <summary>
        ///     List of internal feature flags for this store.
        /// </summary>
        [JsonProperty("feature_flags")]
        public string[] FeatureFlags { get; set; }

        /// <summary>
        ///     A summary of the store’s terms.
        /// </summary>
        [JsonProperty("brief_terms")]
        public TictailBriefTerms BriefTerms { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was created.
        /// </summary>
        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        ///     Requires auth. Contact email for customers.
        /// </summary>
        [JsonProperty("contact_email")]
        public string ContactEmail { get; set; }

        /// <summary>
        ///     Requires auth.  Email to a storekeeper for this store.
        /// </summary>
        [JsonProperty("storekeeper_email")]
        public string StorekeeperEmail { get; set; }

        /// <summary>
        ///     Requires auth.  Whether or not this shop is a sandbox shop.
        /// </summary>
        [JsonProperty("sandbox")]
        public bool Sandbox { get; set; }

        /// <summary>
        ///     Requires auth. Whether or not this shop has opted out of the Tictail marketplace.
        /// </summary>
        [JsonProperty("marketplace_opt_out")]
        public bool MarketplaceOptOut { get; set; }

        /// <summary>
        ///     Requires auth.  TODO.
        /// </summary>
        [JsonProperty("managed")]
        public bool Managed { get; set; }

        /// <summary>
        ///     Requires auth.  Whether or not this shop has enabled HTTPS as the default for their custom shop.
        /// </summary>
        [JsonProperty("default_to_https")]
        public bool DefaultToHttps { get; set; }

        /// <summary>
        ///     Requires auth. Whether or not this shop has a password enabled for its custom shop (through the Password Protection
        ///     app).
        /// </summary>
        [JsonProperty("storefront_password_enabled")]
        public bool StorefrontPasswordEnabled { get; set; }

        /// <summary>
        ///     Requires auth. URL to store’s dashboard.
        /// </summary>
        [JsonProperty("dashboard_url")]
        public string DashboardUrl { get; set; }

        /// <summary>
        ///     Requires auth.The currency used for Tictail App Store purchases.
        /// </summary>
        [JsonProperty("appstore_currency")]
        public string AppstoreCurrency { get; set; }

        /// <summary>
        ///     Requires auth. ID of the internal pricing plan for this store.
        /// </summary>
        [JsonProperty("store_pricing_plan_id")]
        public string StorePricingPlanId { get; set; }

        /// <summary>
        ///     Requires auth.ID of internal tier for this store.
        /// </summary>
        [JsonProperty("store_tier_id")]
        public string StoreTierId { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this product was last modified.
        /// </summary>
        [JsonProperty("launched_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? LaunchedAt { get; set; }
    }
}