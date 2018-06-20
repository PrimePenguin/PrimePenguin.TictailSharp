﻿using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PrimePenguin.TictailSharp.Converters
{
    /// <summary>
    ///     A custom enum converter for all enums which returns the value
    ///     as null when the value is null or does not exist.
    /// </summary>
    public class NullableEnumConverter<T> : StringEnumConverter where T : struct
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (Enum.TryParse(reader.Value?.ToString() ?? "", true, out T parsed)) return parsed;
            // Some EnumMember values have an '_', '-' or '/' in their value and will fail the TryParse or IsDefined checks.
            // Use reflection to pull all of the enums values, get their EnumMember value and check if there's a match.

            var enumType = typeof(T);
            var enumTypeInfo = enumType.GetTypeInfo();
            var enumVals = Enum.GetValues(enumType);

            foreach (var enumVal in enumVals)
            {
                var valInfo = enumTypeInfo.DeclaredMembers;
                var enumMember = valInfo.First().GetCustomAttributes(typeof(EnumMemberAttribute), false);

                if (!enumMember.Any()) continue;

                if (((EnumMemberAttribute) enumMember.First()).Value == reader.Value?.ToString())
                    return (T) enumVal;
            }

            //No match found. Return null.
            return null;
        }
    }
}