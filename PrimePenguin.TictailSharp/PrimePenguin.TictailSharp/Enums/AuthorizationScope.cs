using System.Runtime.Serialization;
using Newtonsoft.Json;
using PrimePenguin.TictailSharp.Converters;

namespace PrimePenguin.TictailSharp.Enums
{
    [JsonConverter(typeof(NullableEnumConverter<TictailAuthorizationScope>))]
    public enum TictailAuthorizationScope
    {
        [EnumMember(Value = "store.navigation.read")] StoreNavigationRead,
        [EnumMember(Value = "store.navigation.update")] StoreNavigationUpdate,
        [EnumMember(Value = "store.navigation.delete")] StoreNavigationDelete,
        [EnumMember(Value = "store.navigation.create")] StoreNavigationCreate,
        [EnumMember(Value = "store.order.read")] StoreOrderRead,
        [EnumMember(Value = "user.order.read")] UserOrderRead,
        [EnumMember(Value = "store.order.cancel")] StoreOrderCancel,
        [EnumMember(Value = "store.order.fulfil")] StoreOrderFulfil,
        [EnumMember(Value = "store.order.refund")] StoreOrderRefund,
        [EnumMember(Value = "store.manage")] StoreManage
    }
}