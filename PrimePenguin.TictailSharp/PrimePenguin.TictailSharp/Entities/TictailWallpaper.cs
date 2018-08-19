using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailWallpaper : TictailObject
    {
        /// <summary>
        ///     The width of the original image, in pixels.
        /// </summary>
        [JsonProperty("original_width")]
        public decimal? OriginalWidth { get; set; }

        /// <summary>
        ///     The height of the original image, in pixels.
        /// </summary>
        [JsonProperty("original_height")]
        public decimal? OriginalHeight { get; set; }

        /// <summary>
        ///     The URL to the original image. See images for how to use this URL to show smaller thumbnails.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

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