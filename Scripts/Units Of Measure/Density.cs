namespace Software10101.Units {
	public struct Density {
		public static readonly Density WATER = 1.0; // g/cm^3

		public readonly Mass mass;
		public readonly Volume volume;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Density (double d) {
			mass = Mass.From(d, Mass.GRAM);
			volume = Volume.CUBIC_CENTIMETER;
		}

		public Density (Mass m, Volume v) {
			mass = m;
			volume = v;
		}

		public static Density From (double p) {
			return new Density(p);
		}

		public static Density From (Mass m, Volume v) {
			return new Density(m, v);
		}

		public static implicit operator Density (double p) {
			return From(p);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Mass mUnit, Volume vUnit) {
			return mass.To(mUnit) / volume.To(vUnit);
		}

		public static implicit operator double (Density p) {
			return p.mass.To(Mass.GRAM) / p.volume.To(Volume.CUBIC_CENTIMETER);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Density operator * (Density first, double second) {
			return new Density(first.mass * second, first.volume);
		}

		public static Density operator * (double first, Density second) {
			return new Density(second.mass * first, second.volume);
		}

		public static double operator / (Density first, Density second) {
			return first.To(Mass.GRAM, Volume.CUBIC_CENTIMETER) / second.To(Mass.GRAM, Volume.CUBIC_CENTIMETER);
		}

		public static Density operator / (Density first, double second) {
			return new Density(first.mass / second, first.volume);
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Mass operator * (Density density, Volume volume) {
			return density.mass * (volume / density.volume);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		override
		public string ToString () {
			return ToStringGramsPerCubicCentimeter();
		}

		public string ToStringGramsPerCubicCentimeter () {
			return To(Mass.GRAM, Volume.CUBIC_CENTIMETER) + "g/cm³";
		}

		public string ToStringKilogramsPerCubicKilometer () {
			return To(Mass.KILOGRAM, Volume.CUBIC_KILOMETER) + "kg/km³";
		}

		public string ToStringKilogramsPerCubicMeter () {
			return To(Mass.KILOGRAM, Volume.CUBIC_METER) + "kg/m³";
		}
	}
}
