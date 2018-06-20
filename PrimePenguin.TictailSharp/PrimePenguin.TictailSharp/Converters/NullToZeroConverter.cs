using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Converters
{
    /// <summary>
    ///     A custom integer converter that converts null to zero
    /// </summary>
    public class NullToZeroConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return string.IsNullOrEmpty(reader.Value?.ToString()) ? 0 : Convert.ToInt32(reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var number = int.Parse(value.ToString());
                writer.WriteValue(number);
            }
        }
    }
}