namespace Software10101.Units {
    public readonly struct Area {
        private const string Unit = "km²";

        public static readonly Area ZeroArea        = 0.0;
        public static readonly Area SquareKilometer = 1.0;
        public static readonly Area MaxArea         = double.MaxValue;

        private readonly double _kmSquared;

        /////////////////////////////////////////////////////////////////////////////
        // BOXING
        /////////////////////////////////////////////////////////////////////////////
        public Area(double a) {
            _kmSquared = a;
        }

        public Area(double a, Area unit) {
            _kmSquared = a * unit._kmSquared;
        }

        public static Area From(double a) {
            return new Area(a);
        }

        public static Area From(double a, Area unit) {
            return new Area(a, unit);
        }

        public static implicit operator Area(double a) {
            return From(a);
        }

        /////////////////////////////////////////////////////////////////////////////
        // UN-BOXING
        /////////////////////////////////////////////////////////////////////////////
        public double To(Area unit) {
            return _kmSquared / unit;
        }

        public static implicit operator double(Area a) {
            return a._kmSquared;
        }

        /////////////////////////////////////////////////////////////////////////////
        // OPERATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Area operator +(Area first, Area second) {
            return first._kmSquared + second._kmSquared;
        }

        public static Area operator +(Area first, double second) {
            return first._kmSquared + second;
        }

        public static Area operator +(double first, Area second) {
            return first + second._kmSquared;
        }

        public static Area operator -(Area first, Area second) {
            return first._kmSquared - second._kmSquared;
        }

        public static Area operator -(Area first, double second) {
            return first._kmSquared - second;
        }

        public static Area operator -(double first, Area second) {
            return first - second._kmSquared;
        }

        public static Area operator *(Area first, double second) {
            return first._kmSquared * second;
        }

        public static Area operator *(double first, Area second) {
            return first * second._kmSquared;
        }

        public static double operator /(Area first, Area second) {
            return first._kmSquared / second._kmSquared;
        }

        public static Area operator /(Area first, double second) {
            return first._kmSquared / second;
        }

        /////////////////////////////////////////////////////////////////////////////
        // MUTATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Length operator /(Area area, Length length) {
            return area._kmSquared / length.To(Length.Kilometer);
        }

        public static Volume operator *(Area area, Length length) {
            return area._kmSquared * length.To(Length.Kilometer);
        }

        /////////////////////////////////////////////////////////////////////////////
        // TO STRING
        /////////////////////////////////////////////////////////////////////////////
        public override string ToString() {
            return ToStringSquareKilometers();
        }

        public string ToStringSquareKilometers() {
            return $"{To(SquareKilometer):F2}{Unit}";
        }
    }
}
