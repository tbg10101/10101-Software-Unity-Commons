namespace Software10101.Units {
    public readonly struct Mass {
        private const string Unit = "g";

        public static readonly Mass ZeroMass    = 0.0;
        public static readonly Mass Gram        = 0.001;
        public static readonly Mass Kilogram    = 1.0;
        public static readonly Mass EarthMass   = 5972200000000000000000000.0;
        public static readonly Mass JupiterMass = 1898000000000000000000000000.0;
        public static readonly Mass SolarMass   = 1988550000000000000000000000000.0;
        public static readonly Mass MaxMass     = double.MaxValue;

        private readonly double _kilograms;

        /////////////////////////////////////////////////////////////////////////////
        // BOXING
        /////////////////////////////////////////////////////////////////////////////
        public Mass(double kg) {
            _kilograms = kg;
        }

        public Mass(double m, Mass unit) {
            _kilograms = m * unit;
        }

        public static Mass From(double kg) {
            return new Mass(kg);
        }

        public static Mass From(double m, Mass unit) {
            return new Mass(m, unit);
        }

        public static implicit operator Mass(double kg) {
            return From(kg);
        }

        /////////////////////////////////////////////////////////////////////////////
        // UN-BOXING
        /////////////////////////////////////////////////////////////////////////////
        public double To(Mass unit) {
            return _kilograms / unit._kilograms;
        }

        public static implicit operator double(Mass m) {
            return m._kilograms;
        }

        /////////////////////////////////////////////////////////////////////////////
        // OPERATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Mass operator +(Mass first, Mass second) {
            return first._kilograms + second._kilograms;
        }

        public static Mass operator +(Mass first, double second) {
            return first._kilograms + second;
        }

        public static Mass operator +(double first, Mass second) {
            return first + second._kilograms;
        }

        public static Mass operator -(Mass first, Mass second) {
            return first._kilograms - second._kilograms;
        }

        public static Mass operator -(Mass first, double second) {
            return first._kilograms - second;
        }

        public static Mass operator -(double first, Mass second) {
            return first - second._kilograms;
        }

        public static Mass operator *(Mass first, double second) {
            return first._kilograms * second;
        }

        public static Mass operator *(double first, Mass second) {
            return first * second._kilograms;
        }

        public static double operator /(Mass first, Mass second) {
            return first._kilograms / second._kilograms;
        }

        public static Mass operator /(Mass first, double second) {
            return first._kilograms / second;
        }

        /////////////////////////////////////////////////////////////////////////////
        // MUTATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Density operator /(Mass mass, Volume volume) {
            return new Density(mass, volume);
        }

        /////////////////////////////////////////////////////////////////////////////
        // TO STRING
        /////////////////////////////////////////////////////////////////////////////
        public override string ToString() {
            return Si.ToLargestSiString(_kilograms, Unit, 2, 3, 0);
        }

        public string ToStringGrams() {
            return Si.ToLargestSiString(_kilograms, Unit, 2, 3, 0, 0);
        }

        public string ToStringKilograms() {
            return Si.ToLargestSiString(_kilograms, Unit, 2, 3, 3, 3);
        }

        public string ToStringEarthMasses() {
            return $"{To(EarthMass):F2}M⊕";
        }

        public string ToStringSolarMasses() {
            return $"{To(SolarMass):F2}M☉";
        }
    }
}
