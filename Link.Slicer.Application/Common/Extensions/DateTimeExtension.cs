namespace Link.Slicer.Application.Common.Extensions
{
    /// <summary>
    /// An extension class for DateTime methods
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Converts timestamp to DateTime.
        /// </summary>
        public static DateTime ToDateTime(this long timestamp)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(timestamp);

            return dt;
        }

        /// <summary>
        /// Converts timestamp to DateTimeOffset.
        /// </summary>
        public static DateTimeOffset ToDateTimeOffset(this long timestamp)
        {
            var dt = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            return dt;
        }

        /// <summary>
        /// Converts DateTime to timestamp.
        /// </summary>
        public static long ToTimestamp(this DateTime dt)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var elapsedTime = dt - epoch;

            return (long)elapsedTime.TotalSeconds;
        }

        /// <summary>
        /// Converts DateTimeOffset to timestamp.
        /// </summary>
        public static long ToTimestamp(this DateTimeOffset dt)
        {
            var elapsedTime = dt.ToUnixTimeSeconds();
            return elapsedTime;
        }

        /// <summary>
        /// Returns the start of a date.
        /// </summary>
        public static DateTimeOffset StartOfDate(this DateTimeOffset dt)
        {
            return dt.Date;
        }

        /// <summary>
        /// Returns the end of a date.
        /// </summary>
        public static DateTimeOffset EndOfDate(this DateTimeOffset dt)
        {
            return dt.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Returns the start of a date.
        /// </summary>
        public static DateTime StartOfDate(this DateTime dt)
        {
            return dt.Date;
        }

        /// <summary>
        /// Returns the end of a date.
        /// </summary>
        public static DateTime EndOfDate(this DateTime dt)
        {
            return dt.Date.AddDays(1).AddTicks(-1);
        }
    }
}
