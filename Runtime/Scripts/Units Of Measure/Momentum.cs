namespace Software10101.Units {
    public readonly struct Momentum {
        private const string Unit = "kg⋅m/s";

        public static readonly Momentum ZeroMomentum = 0.0;
        public static readonly Momentum KilogramMeterPerSecond = 1.0;
        public static readonly Momentum MaxMomentum  = double.MaxValue;

        private readonly Mass _mass;
        private readonly Speed _speed;

        /////////////////////////////////////////////////////////////////////////////
        // BOXING
        /////////////////////////////////////////////////////////////////////////////
        public Momentum(double d) {
            _mass = Mass.From(d, Mass.Kilogram);
            _speed = Speed.MeterPerSecond;
        }

        public Momentum(Mass m, Speed s) {
            _mass = m;
            _speed = s;
        }

        public Momentum(double m, Momentum unit) {
            _mass = m * unit._mass;
            _speed = unit._speed;
        }

        public static Momentum From(double p) {
            return new Momentum(p);
        }

        public static Momentum From(Mass m, Speed s) {
            return new Momentum(m, s);
        }

        public static Momentum From(double m, Momentum unit) {
            return new Momentum(m, unit);
        }

        public static implicit operator Momentum(double p) {
            return From(p);
        }

        /////////////////////////////////////////////////////////////////////////////
        // UN-BOXING
        /////////////////////////////////////////////////////////////////////////////
        public double To(Momentum unit) {
            return _mass.To(unit._mass) * _speed.To(unit._speed);
        }

        public double To(Mass mUnit, Speed sUnit) {
            return _mass.To(mUnit) * _speed.To(sUnit);
        }

        public static implicit operator double(Momentum p) {
            return p._mass.To(Mass.Kilogram) * p._speed.To(Speed.MeterPerSecond);
        }

        /////////////////////////////////////////////////////////////////////////////
        // OPERATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Momentum operator *(Momentum first, double second) {
            return new Momentum(first._mass * second, first._speed);
        }

        public static Momentum operator *(double first, Momentum second) {
            return new Momentum(second._mass * first, second._speed);
        }

        public static double operator /(Momentum first, Momentum second) {
            return first.To(Mass.Kilogram, Speed.MeterPerSecond) / second.To(Mass.Kilogram, Speed.MeterPerSecond);
        }

        public static Momentum operator /(Momentum first, double second) {
            return new Momentum(first._mass / second, first._speed);
        }

        /////////////////////////////////////////////////////////////////////////////
        // MUTATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Mass operator /(Momentum momentum, Speed speed) {
            return new Mass(
                momentum.To(KilogramMeterPerSecond) / speed.To(Speed.MeterPerSecond),
                Mass.Kilogram);
        }

        public static Speed operator /(Momentum momentum, Mass mass) {
            return new Speed(
                momentum.To(KilogramMeterPerSecond) / mass.To(Mass.Kilogram),
                Speed.MeterPerSecond);
        }

        /////////////////////////////////////////////////////////////////////////////
        // TO STRING
        /////////////////////////////////////////////////////////////////////////////
        public override string ToString() {
            return ToStringKilogramMetersPerSecond();
        }

        public string ToStringKilogramMetersPerSecond() {
            return $"{To(Mass.Kilogram, Speed.MeterPerSecond):F2}{Unit}";
        }
    }
}
