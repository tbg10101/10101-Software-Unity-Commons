namespace Software10101.Units {
	public struct Duration {
		private const string UNIT = "s";
		
		public static readonly Duration ZERO_TIME =     0.0;           // s
		public static readonly Duration NANOSECOND =    0.000000001;   // s
		public static readonly Duration MICROSECOND =   0.000001;      // s
		public static readonly Duration MILLISECOND =   0.001;         // s
		public static readonly Duration SECOND =        1.0;           // s
		public static readonly Duration MINUTE =       60.0;           // s
		public static readonly Duration HOUR =         60.0 * MINUTE;  // s
		public static readonly Duration DAY =          24.0 * HOUR;    // s
		public static readonly Duration WEEK =          7.0 * DAY;     // s
		public static readonly Duration YEAR =        365.24 * DAY;    // s
		public static readonly Duration DECADE =       10.0 * YEAR;    // s
		public static readonly Duration CENTURY =      10.0 * DECADE;  // s
		public static readonly Duration MILLENIUM =    10.0 * CENTURY; // s
		public static readonly Duration MAX_TIME = double.MaxValue;

		private readonly double seconds;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Duration (double s) {
			seconds = s;
		}

		public Duration (double d, Duration unit) {
			seconds = d * unit.seconds;
		}

		public static Duration From (double s) {
			return new Duration(s);
		}

		public static Duration From (double d, Duration unit) {
			return new Duration(d, unit);
		}

		public static implicit operator Duration (double s) {
			return From(s);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Duration unit) {
			return seconds / unit.seconds;
		}

		public static implicit operator double (Duration d) {
			return d.seconds;
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Duration operator + (Duration first, Duration second) {
			return first.seconds + second.seconds;
		}

		public static Duration operator + (Duration first, double second) {
			return first.seconds + second;
		}

		public static Duration operator + (double first, Duration second) {
			return first + second.seconds;
		}

		public static Duration operator + (Duration first, float second) {
			return first.seconds + second;
		}

		public static Duration operator + (float first, Duration second) {
			return first + second.seconds;
		}

		public static Duration operator - (Duration first, Duration second) {
			return first.seconds - second.seconds;
		}

		public static Duration operator - (Duration first, double second) {
			return first.seconds - second;
		}

		public static Duration operator - (double first, Duration second) {
			return first - second.seconds;
		}

		public static Duration operator - (Duration first, float second) {
			return first.seconds - second;
		}

		public static Duration operator - (float first, Duration second) {
			return first - second.seconds;
		}

		public static Duration operator * (Duration first, double second) {
			return first.seconds * second;
		}

		public static Duration operator * (double first, Duration second) {
			return first * second.seconds;
		}

		public static Duration operator * (Duration first, float second) {
			return first.seconds * second;
		}

		public static Duration operator * (float first, Duration second) {
			return first * second.seconds;
		}

		public static double operator / (Duration first, Duration second) {
			return first.seconds / second.seconds;
		}

		public static Duration operator / (Duration first, double second) {
			return first.seconds / second;
		}

		public static Duration operator / (Duration first, float second) {
			return first.seconds / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		override
		public string ToString () {
			return SI.ToLargestSiString(seconds, UNIT);
		}

		public string ToStringNanoseconds () {
			return SI.ToLargestSiString(seconds, UNIT, 2, 0, -9, -9);
		}

		public string ToStringMicroseconds () {
			return SI.ToLargestSiString(seconds, UNIT, 2, 0, -6, -6);
		}

		public string ToStringMilliseconds () {
			return SI.ToLargestSiString(seconds, UNIT, 2, 0, -3, -3);
		}

		public string ToStringSeconds () {
			return SI.ToLargestSiString(seconds, UNIT, 2, 0, 0, 0);
		}

		public string ToStringMinutes () {
			return FormatNonSiTime(MINUTE, "minute");
		}

		public string ToStringHours () {
			return FormatNonSiTime(HOUR, "hour");
		}

		public string ToStringDays () {
			return FormatNonSiTime(DAY, "day");
		}

		public string ToStringWeeks () {
			return FormatNonSiTime(WEEK, "week");
		}

		public string ToStringYears () {
			return FormatNonSiTime(YEAR, "year");
		}

		public string ToStringDecades () {
			return FormatNonSiTime(DECADE, "decade");
		}

		public string ToStringCenturies () {
			return FormatNonSiTime(CENTURY, "centur", "ies", "y");
		}

		public string ToStringMillenia () {
			return FormatNonSiTime(MILLENIUM, "milleni", "a", "um");
		}

		private string FormatNonSiTime (Duration unit, string unitString, string pluralSuffix = "s", string singularSuffix = "") {
			string formattedNumber = string.Format("{0:F2}", To(unit));
			return formattedNumber + " " + unitString + (formattedNumber.Equals("1.00") ? singularSuffix : pluralSuffix);
		}
	}
}
