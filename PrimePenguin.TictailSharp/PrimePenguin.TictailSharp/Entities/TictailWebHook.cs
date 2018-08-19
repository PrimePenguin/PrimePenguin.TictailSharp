using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailWebhook : TictailObject
    {
        /// <summary>
        ///     ID for application owning the hook.
        /// </summary>
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        /// <summary>
        ///     The URL to the original image. See images for how to use this URL to show smaller thumbnails.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        ///     Current hook status, can be one of enabled, disabled, or deleted.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this image was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        ///     The UTC date and time in ISO 8601 format format when this image was last modified.
        /// </summary>
        [JsonProperty("modified_at")]
        public DateTimeOffset? ModifiedAt { get; set; }
    }
}