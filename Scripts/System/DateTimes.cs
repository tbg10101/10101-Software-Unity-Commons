namespace System {
    public static class DateTimes {
        public static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Creates a new DateTime from the number of seonds since midnight on January 1st, 1970.
        /// </summary>
        /// <returns>DateTime from Unix epoch seconds.</returns>
        /// <param name="unixTime">Unix epoch seconds.</param>
        public static DateTime FromUnixTime (this long unixTime) {
            return EPOCH.AddSeconds(unixTime);
        }

        public static long ToUnixTime(this DateTime date) {
            return Convert.ToInt64((date.ToUniversalTime() - EPOCH).TotalSeconds);
        }
    }
}
