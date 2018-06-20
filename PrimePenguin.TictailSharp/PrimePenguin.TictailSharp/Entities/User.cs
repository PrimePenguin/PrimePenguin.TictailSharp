using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class User : TictailObject
    {
        /// <summary>
        ///     Unique but mutable username.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        ///     Full name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     User’s email address.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        ///     User avatar information.
        /// </summary>
        [JsonProperty("avatar")]
        public ProductImage Avatar { get; set; }

        /// <summary>
        ///     Terms of Service version accepted by the user.
        /// </summary>
        [JsonProperty("tos_version")]
        public long? TosVersion { get; set; }
    }
}