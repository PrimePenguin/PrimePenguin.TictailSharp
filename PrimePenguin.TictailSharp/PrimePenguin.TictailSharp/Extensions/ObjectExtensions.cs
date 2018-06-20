using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace PrimePenguin.TictailSharp.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Converts the object to a dictionary./>
        /// </summary>
        /// <returns>The object as a <see cref="IDictionary{String, Object}" />.</returns>
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            IDictionary<string, object> output = new Dictionary<string, object>();
            foreach (var property in obj.GetType().GetAllDeclaredProperties())
            {
                var value = property.GetValue(obj, null);
                var propName = property.Name;
                if (value == null) continue;
                if (property.CustomAttributes.Any(x => x.AttributeType == typeof(JsonPropertyAttribute)))
                {
                    //Get the JsonPropertyAttribute for this property, which will give us its JSON name
                    var attribute = property.GetCustomAttributes(typeof(JsonPropertyAttribute), false)
                        .Cast<JsonPropertyAttribute>().FirstOrDefault();

                    propName = attribute != null ? attribute.PropertyName : property.Name;
                }

                if (value.GetType().GetTypeInfo().IsEnum) value = ((Enum) value).ToSerializedString();

                output.Add(propName, value);
            }

            return output;
        }
    }
}