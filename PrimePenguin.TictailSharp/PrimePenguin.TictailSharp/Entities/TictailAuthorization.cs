using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Entities
{
    public class Root
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
        [JsonProperty("store")]
        public TictailStore Store { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}