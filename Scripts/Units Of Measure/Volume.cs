namespace Software10101.Units {
	public struct Volume {
		private const string UNIT = "km³";
		
		public static readonly Volume ZERO_VOLUME =      0.0;               // km³
		public static readonly Volume CUBIC_CENTIMETER = 0.000000000000001; // km³
		public static readonly Volume CUBIC_METER =      0.000000001;       // km³
		public static readonly Volume CUBIC_KILOMETER =  1.0;               // km³
		public static readonly Volume MAX_VOLUME = double.MaxValue;

		private readonly double kmCubed;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Volume (double km3) {
			kmCubed = km3;
		}

		public Volume (double v, Volume unit) {
			kmCubed = v * unit;
		}

		public static Volume From (double km3) {
			return new Volume(km3);
		}

		public static Volume From (double v, Volume unit) {
			return new Volume(v, unit);
		}

		public static implicit operator Volume (double km3) {
			return From(km3);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Volume unit) {
			return kmCubed / unit.kmCubed;
		}

		public static implicit operator double (Volume v) {
			return v.kmCubed;
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Volume operator + (Volume first, Volume second) {
			return first.kmCubed + second.kmCubed;
		}

		public static Volume operator + (Volume first, double second) {
			return first.kmCubed + second;
		}

		public static Volume operator + (double first, Volume second) {
			return first + second.kmCubed;
		}

		public static Volume operator - (Volume first, Volume second) {
			return first.kmCubed - second.kmCubed;
		}

		public static Volume operator - (Volume first, double second) {
			return first.kmCubed - second;
		}

		public static Volume operator - (double first, Volume second) {
			return first - second.kmCubed;
		}

		public static Volume operator * (Volume first, double second) {
			return first.kmCubed * second;
		}

		public static Volume operator * (double first, Volume second) {
			return first * second.kmCubed;
		}

		public static double operator / (Volume first, Volume second) {
			return first.kmCubed / second.kmCubed;
		}

		public static Volume operator / (Volume first, double second) {
			return first.kmCubed / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Area operator / (Volume volume, Length length) {
			return volume.kmCubed / length.To(Length.KILOMETER);
		}

		public static Length operator / (Volume volume, Area area) {
			return volume.kmCubed / area.To(Area.SQUARE_KILOMETER);
		}

		public static Mass operator * (Volume volume, Density density) {
			return density.mass.To(Mass.KILOGRAM) / density.volume.To(Volume.CUBIC_CENTIMETER) * volume.To(Volume.CUBIC_CENTIMETER);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		override
		public string ToString () {
			return ToStringCubicKilometers();
		}

		public string ToStringCubicCentimeters () {
			return string.Format("{0:F2}{1}", To(CUBIC_CENTIMETER), "cm³");
		}

		public string ToStringCubicMeters () {
			return string.Format("{0:F2}{1}", To(CUBIC_METER), "m³");
		}

		public string ToStringCubicKilometers () {
			return string.Format("{0:F2}{1}", kmCubed, UNIT);
		}
	}
}
