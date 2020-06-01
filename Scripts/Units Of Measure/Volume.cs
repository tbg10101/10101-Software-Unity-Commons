namespace Software10101.Units {
    public readonly struct Volume {
        private const string Unit = "km³";

        public static readonly Volume ZeroVolume      = 0.0;
        public static readonly Volume CubicCentimeter = 0.000000000000001;
        public static readonly Volume CubicMeter      = 0.000000001;
        public static readonly Volume CubicKilometer  = 1.0;
        public static readonly Volume MaxVolume       = double.MaxValue;

        private readonly double _kmCubed;

        /////////////////////////////////////////////////////////////////////////////
        // BOXING
        /////////////////////////////////////////////////////////////////////////////
        public Volume(double km3) {
            _kmCubed = km3;
        }

        public Volume(double v, Volume unit) {
            _kmCubed = v * unit;
        }

        public static Volume From(double km3) {
            return new Volume(km3);
        }

        public static Volume From(double v, Volume unit) {
            return new Volume(v, unit);
        }

        public static implicit operator Volume(double km3) {
            return From(km3);
        }

        /////////////////////////////////////////////////////////////////////////////
        // UN-BOXING
        /////////////////////////////////////////////////////////////////////////////
        public double To(Volume unit) {
            return _kmCubed / unit._kmCubed;
        }

        public static implicit operator double(Volume v) {
            return v._kmCubed;
        }

        /////////////////////////////////////////////////////////////////////////////
        // OPERATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Volume operator +(Volume first, Volume second) {
            return first._kmCubed + second._kmCubed;
        }

        public static Volume operator +(Volume first, double second) {
            return first._kmCubed + second;
        }

        public static Volume operator +(double first, Volume second) {
            return first + second._kmCubed;
        }

        public static Volume operator -(Volume first, Volume second) {
            return first._kmCubed - second._kmCubed;
        }

        public static Volume operator -(Volume first, double second) {
            return first._kmCubed - second;
        }

        public static Volume operator -(double first, Volume second) {
            return first - second._kmCubed;
        }

        public static Volume operator *(Volume first, double second) {
            return first._kmCubed * second;
        }

        public static Volume operator *(double first, Volume second) {
            return first * second._kmCubed;
        }

        public static double operator /(Volume first, Volume second) {
            return first._kmCubed / second._kmCubed;
        }

        public static Volume operator /(Volume first, double second) {
            return first._kmCubed / second;
        }

        /////////////////////////////////////////////////////////////////////////////
        // MUTATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Area operator /(Volume volume, Length length) {
            return new Area(
                volume.To(CubicKilometer) / length.To(Length.Kilometer),
                Area.SquareKilometer);
        }

        public static Length operator /(Volume volume, Area area) {
            return new Length(
                volume.To(CubicKilometer) / area.To(Area.SquareKilometer),
                Length.Kilometer);
        }

        public static Mass operator *(Volume volume, Density density) {
            return new Mass(
                volume.To(CubicKilometer) * density.To(Mass.Kilogram, CubicKilometer),
                Mass.Kilogram);
        }

        /////////////////////////////////////////////////////////////////////////////
        // TO STRING
        /////////////////////////////////////////////////////////////////////////////
        public override string ToString() {
            return ToStringCubicKilometers();
        }

        public string ToStringCubicCentimeters() {
            return $"{To(CubicCentimeter):F2}cm³";
        }

        public string ToStringCubicMeters() {
            return $"{To(CubicMeter):F2}m³";
        }

        public string ToStringCubicKilometers() {
            return $"{_kmCubed:F2}{Unit}";
        }
    }
}
