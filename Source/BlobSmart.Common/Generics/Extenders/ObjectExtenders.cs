﻿using System.Diagnostics;

namespace BlobSmart.Common.Generics
{
    public static partial class Extenders
    {
        [DebuggerHidden]
        public static bool IsDefault<T>(this T value)
        {
            return (Equals(value, default(T)));
        }

        [DebuggerHidden]
        public static string ToNewString<T>(this T value)
        {
            return string.Copy(value.ToString());
        }
    }
}