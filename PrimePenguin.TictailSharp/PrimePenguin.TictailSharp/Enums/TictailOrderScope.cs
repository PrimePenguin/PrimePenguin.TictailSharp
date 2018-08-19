using System.Runtime.Serialization;
using Newtonsoft.Json;
using PrimePenguin.TictailSharp.Converters;

namespace PrimePenguin.TictailSharp.Enums
{
    [JsonConverter(typeof(NullableEnumConverter<TictailOrderScope>))]
    public enum TictailOrderScope
    {
        [EnumMember(Value = "items")] Items,
        [EnumMember(Value = "internal_notes")] InternalNotes,
        [EnumMember(Value = "fulfillment")] TictailOrderFulfillment,
        [EnumMember(Value = "shipping_line")] ShippingLine,
        [EnumMember(Value = "adjustments")] Adjustments,
        [EnumMember(Value = "shipping_address")] TictailShippingAddress,
        [EnumMember(Value = "transactions")] Tictailtransactions,
        [EnumMember(Value = "disputes")] TictailDisputes,
        [EnumMember(Value = "messages")] TictailMessages,
        [EnumMember(Value = "totals")] Totals
    }
}