namespace Software10101.Units {
	public struct Mass {
		private const string UNIT = "g";

		public static readonly Mass GRAM =                                       0.001; // kg
		public static readonly Mass KILOGRAM =                                   1.0; // kg
		public static readonly Mass EARTH_MASS =         5972200000000000000000000.0; // kg
		public static readonly Mass JUPITER_MASS =    1898000000000000000000000000.0; // kg
		public static readonly Mass SOLAR_MASS =   1988550000000000000000000000000.0; // kg

		private readonly double kilograms;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Mass (double kg) {
			kilograms = kg;
		}

		public Mass (double m, Mass unit) {
			kilograms = m * unit;
		}

		public static Mass From (double kg) {
			return new Mass(kg);
		}

		public static Mass From (double m, Mass unit) {
			return new Mass(m, unit);
		}

		public static implicit operator Mass (double kg) {
			return From(kg);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Mass unit) {
			return kilograms / unit.kilograms;
		}

		public static implicit operator double (Mass m) {
			return m.kilograms;
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Mass operator + (Mass first, Mass second) {
			return first.kilograms + second.kilograms;
		}

		public static Mass operator + (Mass first, double second) {
			return first.kilograms + second;
		}

		public static Mass operator + (double first, Mass second) {
			return first + second.kilograms;
		}

		public static Mass operator - (Mass first, Mass second) {
			return first.kilograms - second.kilograms;
		}

		public static Mass operator - (Mass first, double second) {
			return first.kilograms - second;
		}

		public static Mass operator - (double first, Mass second) {
			return first - second.kilograms;
		}

		public static Mass operator * (Mass first, double second) {
			return first.kilograms * second;
		}

		public static Mass operator * (double first, Mass second) {
			return first * second.kilograms;
		}

		public static double operator / (Mass first, Mass second) {
			return first.kilograms / second.kilograms;
		}

		public static Mass operator / (Mass first, double second) {
			return first.kilograms / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Density operator / (Mass mass, Volume volume) {
			return new Density(mass, volume);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		override
		public string ToString () {
			return SI.ToLargestSiString(kilograms, UNIT, 2, 3, 0);
		}

		public string ToStringGrams () {
			return SI.ToLargestSiString(kilograms, UNIT, 2, 3, 0, 0);
		}

		public string ToStringKilograms () {
			return SI.ToLargestSiString(kilograms, UNIT, 2, 3, 3, 3);
		}

		public string ToStringEarthMasses () {
			return To(EARTH_MASS) + "M⊕";
		}

		public string ToStringSolarMasses () {
			return To(SOLAR_MASS) + "M☉";
		}
	}
}
