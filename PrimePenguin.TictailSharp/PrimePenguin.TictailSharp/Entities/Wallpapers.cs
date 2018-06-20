using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class Wallpapers
    {
        /// <summary>
        ///     The store’s wallpaper (the term iphone is for legacy reasons).
        /// </summary>
        [JsonProperty("iphone")]
        public Wallpaper Iphone { get; set; }
    }
}