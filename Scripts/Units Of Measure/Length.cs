namespace Software10101.Units {
	public struct Length {
		public static readonly Length CENTIMETER =                    0.00001; // km
		public static readonly Length METER =                         0.001;   // km
		public static readonly Length KILOMETER =                     1.0;     // km
		public static readonly Length EARTH_RADIUS =               6371.0;     // km
		public static readonly Length SOLAR_RADIUS =             695700.0;     // km
		public static readonly Length ASTRONOMICAL_UNIT =     149597870.7;     // km
		public static readonly Length LIGHT_YEAR =        9460730472580.8;     // km

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

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public string ToStringCentimeters () {
			return To(CENTIMETER) + "cm";
		}

		public string ToStringMeters () {
			return To(METER) + "m";
		}

		public string ToStringKilometers () {
			return kilometers + "km";
		}

		public string ToStringEarthRadii () {
			return To(EARTH_RADIUS) + "R⊕";
		}

		public string ToStringSolarRadii () {
			return To(SOLAR_RADIUS) + "R☉";
		}

		public string ToStringAstronomicalUnits () {
			return To(ASTRONOMICAL_UNIT) + "au";
		}

		public string ToStringLightYears () {
			return To(LIGHT_YEAR) + "ly";
		}
	}
}
