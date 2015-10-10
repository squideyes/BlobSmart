using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BlobSmart.Common.Generics
{
    public static class EnumHelper
    {
        public static string GetDescription(Type enumType, object value)
        {
            Contract.Requires(enumType != null, nameof(enumType));

            if (value == null)
                return string.Empty;

            var fi = enumType.GetField(value.ToString());

            var attributes = (DescriptionAttribute[])
                fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? 
                attributes[0].Description : value.ToString();
        }

        public static List<T> ToList<T>() where T: struct
        {
            Contract.Requires(typeof(T).BaseType == typeof(Enum), "typeof(T).BaseType");

            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}
