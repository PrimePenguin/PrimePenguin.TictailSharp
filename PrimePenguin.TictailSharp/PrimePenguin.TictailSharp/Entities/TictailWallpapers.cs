using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class TictailWallpapers
    {
        /// <summary>
        ///     The store’s wallpaper (the term iphone is for legacy reasons).
        /// </summary>
        [JsonProperty("iphone")]
        public TictailWallpaper Iphone { get; set; }
    }
}