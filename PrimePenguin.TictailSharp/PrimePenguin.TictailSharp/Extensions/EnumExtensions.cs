using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace PrimePenguin.TictailSharp.Extensions
{
    /// <summary>
    ///     Enum Extension Method
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Reads and uses the enum's <see cref="EnumMemberAttribute" /> for serialization.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSerializedString(this Enum input)
        {
            var name = input.ToString();
            var info = input.GetType().GetTypeInfo().DeclaredMembers.Where(i => i.Name == name).ToList();
            if (!info.Any()) return name.ToLower();
            var attribute = info.First().GetCustomAttribute<EnumMemberAttribute>();
            return attribute != null ? attribute.Value : name.ToLower();
        }

        /// <summary>
        ///     Convert list of Enums to a comma seperated string
        /// </summary>
        public static string EnumListToString<T>(IEnumerable<T> enumList)
        {
            var list = new List<string>();
            var enumerable = enumList.ToList();
            if (!enumerable.Any()) return string.Join(",", list);
            list.AddRange(enumerable.Select(enumItem => ToSerializedString(enumItem as Enum)));
            return string.Join(",", list);
        }
    }
}