namespace Software10101.Units {
	public struct Length {
		private const string UNIT = "m";

		public static readonly Length ZERO_LENGTH =                   0.0;     // km
		public static readonly Length CENTIMETER =                    0.00001; // km
		public static readonly Length METER =                         0.001;   // km
		public static readonly Length KILOMETER =                     1.0;     // km
		public static readonly Length EARTH_RADIUS =               6371.0;     // km
		public static readonly Length SOLAR_RADIUS =             695700.0;     // km
		public static readonly Length ASTRONOMICAL_UNIT =     149597870.7;     // km
		public static readonly Length LIGHT_YEAR =        9460730472580.8;     // km
		public static readonly Length MAX_LENGTH = double.MaxValue;

		private readonly double kilometers;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		private Length (double km) {
			kilometers = km;
		}

		private Length (double l, Length unit) {
			kilometers = l * unit;
		}

		public static Length From (double km) {
			return new Length(km);
		}

		public static Length From (double l, Length unit) {
			return new Length(l, unit);
		}

		public static implicit operator Length (double km) {
			return From(km);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Length unit) {
			return kilometers / unit.kilometers;
		}

		public static implicit operator double (Length l) {
			return l.kilometers;
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator + (Length first, Length second) {
			return first.kilometers + second.kilometers;
		}

		public static Length operator + (Length first, double second) {
			return first.kilometers + second;
		}

		public static Length operator + (double first, Length second) {
			return first + second.kilometers;
		}

		public static Length operator - (Length first, Length second) {
			return first.kilometers - second.kilometers;
		}

		public static Length operator - (Length first, double second) {
			return first.kilometers - second;
		}

		public static Length operator - (double first, Length second) {
			return first - second.kilometers;
		}

		public static Length operator * (Length first, double second) {
			return first.kilometers * second;
		}

		public static Length operator * (double first, Length second) {
			return first * second.kilometers;
		}

		public static double operator / (Length first, Length second) {
			return first.kilometers / second.kilometers;
		}

		public static Length operator / (Length first, double second) {
			return first.kilometers / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Area operator * (Length first, Length second) {
			return first.kilometers * second.kilometers;
		}

		public static Volume operator * (Length first, Area second) {
			return first.kilometers * second.To(Area.SQUARE_KILOMETER);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		override
		public string ToString () {
			return SI.ToLargestSiString(kilometers, UNIT, 2, 3, 0);
		}

		public string ToStringCentimeters () {
			return string.Format("{0:F2}{1}{2}", To(CENTIMETER), "c", UNIT);
		}

		public string ToStringMeters () {
			return SI.ToLargestSiString(kilometers, UNIT, 2, 3, 0, 0);
		}

		public string ToStringKilometers () {
			return SI.ToLargestSiString(kilometers, UNIT, 2, 3, 3, 3);
		}

		public string ToStringEarthRadii () {
			return string.Format("{0:F2}{1}", To(EARTH_RADIUS), "R⊕");
		}

		public string ToStringSolarRadii () {
			return string.Format("{0:F2}{1}", To(SOLAR_RADIUS), "R☉");
		}

		public string ToStringAstronomicalUnits () {
			return string.Format("{0:F2}{1}", To(ASTRONOMICAL_UNIT), "au");
		}

		public string ToStringLightYears () {
			return string.Format("{0:F2}{1}", To(LIGHT_YEAR), "ly");
		}
	}
}
