using System.Runtime.Serialization;
using Newtonsoft.Json;
using PrimePenguin.TictailSharp.Converters;

namespace PrimePenguin.TictailSharp.Enums
{
    [JsonConverter(typeof(NullableEnumConverter<AuthorizationScope>))]
    public enum AuthorizationScope
    {
        [EnumMember(Value = "authorization_code")]
        Authorizationcode
    }
}