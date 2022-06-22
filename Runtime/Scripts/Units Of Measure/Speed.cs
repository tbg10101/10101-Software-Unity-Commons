namespace Software10101.Units {
    public readonly struct Speed {
        private const string Unit = "m/s";

        public static readonly Speed ZeroSpeed      = 0.0;
        public static readonly Speed MeterPerSecond = 1.0;
        public static readonly Speed C              = 299792458.0;
        public static readonly Speed MaxSpeed       = double.MaxValue;

        private readonly Length _length;
        private readonly Duration _duration;

        /////////////////////////////////////////////////////////////////////////////
        // BOXING
        /////////////////////////////////////////////////////////////////////////////
        public Speed(double s) {
            _length = Length.From(s, Length.Meter);
            _duration = Duration.Second;
        }

        public Speed(Length l, Duration d) {
            _length = l;
            _duration = d;
        }

        public Speed(double s, Speed unit) {
            _length = s * unit._length;
            _duration = unit._duration;
        }

        public static Speed From(double s) {
            return new Speed(s);
        }

        public static Speed From(Length l, Duration d) {
            return new Speed(l, d);
        }

        public static Speed From(double s, Speed unit) {
            return new Speed(s, unit);
        }

        public static implicit operator Speed(double s) {
            return From(s);
        }

        /////////////////////////////////////////////////////////////////////////////
        // UN-BOXING
        /////////////////////////////////////////////////////////////////////////////
        public double To(Speed unit) {
            return _length.To(unit._length) / _duration.To(unit._duration);
        }

        public double To(Length lUnit, Duration dUnit) {
            return _length.To(lUnit) / _duration.To(dUnit);
        }

        public static implicit operator double(Speed s) {
            return s._length.To(Length.Meter) / s._duration.To(Duration.Second);
        }

        /////////////////////////////////////////////////////////////////////////////
        // OPERATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Speed operator +(Speed first, Speed second) {
            return first.To(MeterPerSecond) + second.To(MeterPerSecond);
        }

        public static Speed operator +(Speed first, double second) {
            return first.To(MeterPerSecond) + second;
        }

        public static Speed operator +(double first, Speed second) {
            return first + second.To(MeterPerSecond);
        }

        public static Speed operator -(Speed first, Speed second) {
            return first.To(MeterPerSecond) - second.To(MeterPerSecond);
        }

        public static Speed operator -(Speed first, double second) {
            return first.To(MeterPerSecond) - second;
        }

        public static Speed operator -(double first, Speed second) {
            return first - second.To(MeterPerSecond);
        }

        public static Speed operator *(Speed first, double second) {
            return first.To(MeterPerSecond) * second;
        }

        public static Speed operator *(double first, Speed second) {
            return first * second.To(MeterPerSecond);
        }

        public static double operator /(Speed first, Speed second) {
            return first.To(MeterPerSecond) / second.To(MeterPerSecond);
        }

        public static Speed operator /(Speed first, double second) {
            return first.To(MeterPerSecond) / second;
        }

        /////////////////////////////////////////////////////////////////////////////
        // MUTATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Length operator *(Speed speed, Duration duration) {
            return new Length(
                speed.To(MeterPerSecond) * duration.To(Duration.Second),
                Length.Meter);
        }

        /////////////////////////////////////////////////////////////////////////////
        // TO STRING
        /////////////////////////////////////////////////////////////////////////////
        public override string ToString() {
            return ToStringMetersPerSecond();
        }

        public string ToStringMetersPerSecond() {
            return $"{To(MeterPerSecond):F2}{Unit}";
        }

        public string ToStringKilometersPerHour() {
            return $"{To(Length.Kilometer, Duration.Hour):F2}km/h";
        }
    }
}
