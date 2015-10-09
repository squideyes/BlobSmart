using System;

namespace BlobSmart.Common
{
    public static class StringExtenders
    {
        public static bool IsGuid(this string value)
        {
            Guid guid;

            return Guid.TryParse(value, out guid);
        }
    }
}
