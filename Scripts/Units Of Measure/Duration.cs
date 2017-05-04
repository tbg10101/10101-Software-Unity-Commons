namespace Software10101.Units {
	public struct Duration {
		public static readonly Duration NANOSECOND =    0.000000001; // s
		public static readonly Duration MICROSECOND =   0.000001; // s
		public static readonly Duration MILLISECOND =   0.001; // s
		public static readonly Duration SECOND =        1.0; // s
		public static readonly Duration MINUTE =       60.0; // s
		public static readonly Duration HOUR =         60.0 * MINUTE; // s
		public static readonly Duration DAY =          24.0 * HOUR; // s
		public static readonly Duration WEEK =          7.0 * DAY; // s
		public static readonly Duration YEAR =        365.24 * DAY; // s
		public static readonly Duration DECADE =       10.0 * YEAR; // s
		public static readonly Duration CENTURY =      10.0 * DECADE; // s
		public static readonly Duration MILLENIUM =    10.0 * CENTURY; // s

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

		public static Duration operator - (Duration first, Duration second) {
			return first.seconds - second.seconds;
		}

		public static Duration operator - (Duration first, double second) {
			return first.seconds - second;
		}

		public static Duration operator - (double first, Duration second) {
			return first - second.seconds;
		}

		public static Duration operator * (Duration first, double second) {
			return first.seconds * second;
		}

		public static Duration operator * (double first, Duration second) {
			return first * second.seconds;
		}

		public static double operator / (Duration first, Duration second) {
			return first.seconds / second.seconds;
		}

		public static Duration operator / (Duration first, double second) {
			return first.seconds / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public string ToStringNanoseconds () {
			return To(NANOSECOND) + "ns";
		}

		public string ToStringMicroseconds () {
			return To(MICROSECOND) + "μs";
		}

		public string ToStringMilliseconds () {
			return To(MILLISECOND) + "ms";
		}

		public string ToStringSeconds () {
			return seconds + " seconds";
		}

		public string ToStringMinutes () {
			return To(MINUTE) + " minutes";
		}

		public string ToStringHours () {
			return To(HOUR) + " hours";
		}

		public string ToStringDays () {
			return To(DAY) + " days";
		}

		public string ToStringWeeks () {
			return To(WEEK) + " weeks";
		}

		public string ToStringYears () {
			return To(YEAR) + " years";
		}

		public string ToStringDecades () {
			return To(DECADE) + " decades";
		}

		public string ToStringCenturies () {
			return To(CENTURY) + " centuries";
		}

		public string ToStringMillenia () {
			return To(MILLENIUM) + " millenia";
		}
	}
}
