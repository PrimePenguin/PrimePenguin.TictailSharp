using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    /// <summary>
    ///     An object representing a Tictail event.
    /// </summary>
    public class TictailMe : TictailObject
    {
        /// <summary>
        ///     The authorized user. May be null if the authorization is only for a store.
        /// </summary>
        [JsonProperty("user")]
        public TictailUser User { get; set; }

        /// <summary>
        ///     List of authorized stores, may include zero or more stores
        /// </summary>
        [JsonProperty("stores")]
        public TictailStore Stores { get; set; }
    }
}