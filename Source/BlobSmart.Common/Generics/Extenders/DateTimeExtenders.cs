using System;

namespace BlobSmart.Common.Generics
{
    public static partial class Extenders
    {
        private const string EST_TZNAME = "Eastern Standard Time";
        private const string UTC_TZNAME = "UTC";

        private static readonly TimeZoneInfo estTzi;
        private static readonly TimeZoneInfo utcTzi;

        static Extenders()
        {
            estTzi = TimeZoneInfo.FindSystemTimeZoneById(EST_TZNAME);
            utcTzi = TimeZoneInfo.FindSystemTimeZoneById(UTC_TZNAME);
        }

        public static DateTime ToEstFromUtc(this DateTime dateTime)
        {
            Contract.Requires(dateTime.Kind == DateTimeKind.Utc, nameof(dateTime));

            return TimeZoneInfo.ConvertTime(dateTime, estTzi);
        }

        public static DateTime ToUtcFromEst(this DateTime dateTime)
        {
            Contract.Requires(dateTime.Kind == DateTimeKind.Unspecified, nameof(dateTime));

            return TimeZoneInfo.ConvertTime(dateTime, estTzi, utcTzi);
        }
    }
}
