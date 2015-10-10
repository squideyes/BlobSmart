using System;
using System.Diagnostics;
using System.Linq;

namespace BlobSmart.Common.Generics
{
    public static partial class Extenders
    {
        [DebuggerHidden]
        public static T ToEnum<T>(this string value) where T : struct
        {
            Contract.Requires(typeof(T).IsEnum, nameof(value));

            return (T)Enum.Parse(typeof(T), value, true);
        }

        [DebuggerHidden]
        public static bool IsDefinedEnumValue<T>(this T value)
        {
            Contract.Requires(typeof(T).IsEnum, nameof(value));

            dynamic dyn = value;

            var max = Enum.GetValues(value.GetType()).
                Cast<dynamic>().Aggregate((e1, e2) => e1 | e2);

            return (max & dyn) == dyn;
        }

        public static bool IsParsableEnumName<T>(this string value)
            where T : struct
        {
            T enumeration;

            return Enum.TryParse(value, true, out enumeration);
        }
    }
}
