using System;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Converters
{
    /// <inheritdoc />
    /// <summary>
    ///     A custom boolean converter that converts False to null and null to False.
    /// </summary>
    public class FalseToNullConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.Value?.ToString() == null || reader.Value?.ToString() == "") return false;
            if (!bool.TryParse(reader.Value.ToString(), out var output))
                throw new JsonReaderException($"Cannot convert given JSON value with {nameof(FalseToNullConverter)}.");
            return output;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var boolean = bool.Parse(value.ToString());

                if (boolean == false)
                    writer.WriteNull();
                else
                    writer.WriteValue(true);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(string)) return true;

            if (objectType == typeof(bool)) return true;

            return objectType == typeof(Nullable);
        }
    }
}