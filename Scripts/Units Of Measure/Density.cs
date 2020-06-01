namespace Software10101.Units {
    public readonly struct Density {
        private const string Unit = "g/cm³";

        public static readonly Density ZeroDensity            = 0.0;
        public static readonly Density Water                  = 1.0;
        public static readonly Density GramPerCentimeterCubed = 1.0;
        public static readonly Density MaxDensity             = double.MaxValue;

        private readonly Mass _mass;
        private readonly Volume _volume;

        /////////////////////////////////////////////////////////////////////////////
        // BOXING
        /////////////////////////////////////////////////////////////////////////////
        public Density(double d) {
            _mass = Mass.From(d, Mass.Gram);
            _volume = Volume.CubicCentimeter;
        }

        public Density(Mass m, Volume v) {
            _mass = m;
            _volume = v;
        }

        public Density(double d, Density unit) {
            _mass = d * unit._mass;
            _volume = unit._volume;
        }

        public static Density From(double p) {
            return new Density(p);
        }

        public static Density From(Mass m, Volume v) {
            return new Density(m, v);
        }

        public static Density From(double d, Density unit) {
            return new Density(d, unit);
        }

        public static implicit operator Density(double p) {
            return From(p);
        }

        /////////////////////////////////////////////////////////////////////////////
        // UN-BOXING
        /////////////////////////////////////////////////////////////////////////////
        public double To(Density unit) {
            return _mass.To(unit._mass) / _volume.To(unit._volume);
        }

        public double To(Mass mUnit, Volume vUnit) {
            return _mass.To(mUnit) / _volume.To(vUnit);
        }

        public static implicit operator double(Density p) {
            return p._mass.To(Mass.Gram) / p._volume.To(Volume.CubicCentimeter);
        }

        /////////////////////////////////////////////////////////////////////////////
        // OPERATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Density operator *(Density first, double second) {
            return new Density(first._mass * second, first._volume);
        }

        public static Density operator *(double first, Density second) {
            return new Density(second._mass * first, second._volume);
        }

        public static double operator /(Density first, Density second) {
            return first.To(Mass.Gram, Volume.CubicCentimeter) / second.To(Mass.Gram, Volume.CubicCentimeter);
        }

        public static Density operator /(Density first, double second) {
            return new Density(first._mass / second, first._volume);
        }

        /////////////////////////////////////////////////////////////////////////////
        // MUTATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Mass operator *(Density density, Volume volume) {
            return new Mass(
                volume.To(Volume.CubicKilometer) * density.To(Mass.Kilogram, Volume.CubicKilometer),
                Mass.Kilogram);
        }

        /////////////////////////////////////////////////////////////////////////////
        // TO STRING
        /////////////////////////////////////////////////////////////////////////////
        public override string ToString () {
            return ToStringGramsPerCubicCentimeter();
        }

        public string ToStringGramsPerCubicCentimeter() {
            return $"{To(Mass.Gram, Volume.CubicCentimeter):F2}{Unit}";
        }

        public string ToStringKilogramsPerCubicKilometer() {
            return $"{To(Mass.Kilogram, Volume.CubicKilometer):F2}kg/km³";
        }

        public string ToStringKilogramsPerCubicMeter() {
            return $"{To(Mass.Kilogram, Volume.CubicMeter):F2}kg/m³";
        }
    }
}
