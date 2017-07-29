namespace Software10101.Units {
	public struct Temperature {
		private const string UNIT = "K";
		
		public static Temperature ABSOLUTE_ZERO =   0.0;  // K
		public static Temperature FREEZING =      273.15; // K
		public static Temperature BOILING =       373.15; // K
		public static Temperature MAX_TEMPERATURE = double.MaxValue;

		private readonly double kelvin;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Temperature (double k) {
			kelvin = k;
		}

		public static Temperature FromKelvin (double k) {
			return new Temperature(k);
		}

		public static Temperature FromCelsius (double c) {
			return new Temperature(c + 273.15);
		}

		public static Temperature FromFahrenheit (double f) {
			return new Temperature((f + 459.67) * (5.0 / 9.0));
		}

		public static implicit operator Temperature (double k) {
			return FromKelvin(k);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double ToKelvin () {
			return kelvin;
		}

		public double ToCelsius () {
			return kelvin - 273.15;
		}

		public double ToFahrenheit () {
			return kelvin * (9.0 / 5.0) - 459.67;
		}

		public static implicit operator double (Temperature t) {
			return t.ToKelvin();
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Temperature operator + (Temperature first, Temperature second) {
			return first.kelvin + second.kelvin;
		}

		public static Temperature operator + (Temperature first, double second) {
			return first.kelvin + second;
		}

		public static Temperature operator + (double first, Temperature second) {
			return first + second.kelvin;
		}

		public static Temperature operator - (Temperature first, Temperature second) {
			return first.kelvin - second.kelvin;
		}

		public static Temperature operator - (Temperature first, double second) {
			return first.kelvin - second;
		}

		public static Temperature operator - (double first, Temperature second) {
			return first - second.kelvin;
		}

		public static Temperature operator * (Temperature first, double second) {
			return first.kelvin * second;
		}

		public static Temperature operator * (double first, Temperature second) {
			return first * second.kelvin;
		}

		public static Temperature operator / (Temperature first, double second) {
			return first.kelvin / second;
		}

		public static double operator / (Temperature first, Temperature second) {
			return first.kelvin / second.kelvin;
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		override
		public string ToString () {
			return ToStringKelvin();
		}

		public string ToStringKelvin () {
			return string.Format("{0:F2}{1}", ToKelvin(), UNIT);
		}

		public string ToStringCelsius () {
			return string.Format("{0:F2}{1}", ToCelsius(), "°C");
		}

		public string ToStringFahrenheit () {
			return string.Format("{0:F2}{1}", ToFahrenheit(), "°F");
		}
	}
}
