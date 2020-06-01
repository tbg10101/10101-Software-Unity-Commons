namespace Software10101.Units {
    public readonly struct VolumetricFlowRate {
        private const string Unit = "m³/s";

        public static readonly VolumetricFlowRate ZeroRate             = 0.0;
        public static readonly VolumetricFlowRate MetersCubedPerSecond = 1.0;

        private readonly Volume _volume;
        private readonly Duration _duration;

        /////////////////////////////////////////////////////////////////////////////
        // BOXING
        /////////////////////////////////////////////////////////////////////////////
        public VolumetricFlowRate(double q) {
            _volume = Volume.From(q, Volume.CubicMeter);
            _duration = Duration.Second;
        }

        public VolumetricFlowRate(Volume v, Duration d) {
            _volume = v;
            _duration = d;
        }

        public VolumetricFlowRate(double v, VolumetricFlowRate unit) {
            _volume = v * unit._volume;
            _duration = unit._duration;
        }

        public static VolumetricFlowRate From(double q) {
            return new VolumetricFlowRate(1);
        }

        public static VolumetricFlowRate From(Volume v, Duration d) {
            return new VolumetricFlowRate(v, d);
        }

        public static VolumetricFlowRate From(double v, VolumetricFlowRate unit) {
            return new VolumetricFlowRate(v, unit);
        }

        public static implicit operator VolumetricFlowRate(double q) {
            return From(q);
        }

        /////////////////////////////////////////////////////////////////////////////
        // UN-BOXING
        /////////////////////////////////////////////////////////////////////////////
        public double To(VolumetricFlowRate unit) {
            return _volume.To(unit._volume) / _duration.To(unit._duration);
        }

        public double To(Volume vUnit, Duration dUnit) {
            return _volume.To(vUnit) / _duration.To(dUnit);
        }

        public static implicit operator double(VolumetricFlowRate q) {
            return q._volume.To(Volume.CubicMeter) / q._duration.To(Duration.Second);
        }

        /////////////////////////////////////////////////////////////////////////////
        // OPERATORS
        /////////////////////////////////////////////////////////////////////////////
        public static VolumetricFlowRate operator +(VolumetricFlowRate first, VolumetricFlowRate second) {
            return first.To(Volume.CubicMeter, Duration.Second) + second.To(Volume.CubicMeter, Duration.Second);
        }

        public static VolumetricFlowRate operator +(VolumetricFlowRate first, double second) {
            return first.To(Volume.CubicMeter, Duration.Second) + second;
        }

        public static VolumetricFlowRate operator +(double first, VolumetricFlowRate second) {
            return first + second.To(Volume.CubicMeter, Duration.Second);
        }

        public static VolumetricFlowRate operator -(VolumetricFlowRate first, VolumetricFlowRate second) {
            return first.To(Volume.CubicMeter, Duration.Second) - second.To(Volume.CubicMeter, Duration.Second);
        }

        public static VolumetricFlowRate operator -(VolumetricFlowRate first, double second) {
            return first.To(Volume.CubicMeter, Duration.Second) - second;
        }

        public static VolumetricFlowRate operator -(double first, VolumetricFlowRate second) {
            return first - second.To(Volume.CubicMeter, Duration.Second);
        }

        public static VolumetricFlowRate operator *(VolumetricFlowRate first, double second) {
            return first.To(Volume.CubicMeter, Duration.Second) * second;
        }

        public static VolumetricFlowRate operator *(double first, VolumetricFlowRate second) {
            return first * second.To(Volume.CubicMeter, Duration.Second);
        }

        public static double operator /(VolumetricFlowRate first, VolumetricFlowRate second) {
            return first.To(Volume.CubicMeter, Duration.Second) / second.To(Volume.CubicMeter, Duration.Second);
        }

        public static VolumetricFlowRate operator /(VolumetricFlowRate first, double second) {
            return first.To(Volume.CubicMeter, Duration.Second) / second;
        }

        /////////////////////////////////////////////////////////////////////////////
        // MUTATORS
        /////////////////////////////////////////////////////////////////////////////
        public static Volume operator *(VolumetricFlowRate volumetricFlowRate, Duration duration) {
            return new Volume(
                volumetricFlowRate.To(Volume.CubicKilometer, Duration.Second) * duration.To(Duration.Second),
                Volume.CubicKilometer);
        }

        /////////////////////////////////////////////////////////////////////////////
        // TO STRING
        /////////////////////////////////////////////////////////////////////////////
        public override string ToString() {
            return ToStringMetersCubedPerSecond();
        }

        public string ToStringMetersCubedPerSecond() {
            return $"{To(Volume.CubicMeter, Duration.Second):F2}{Unit}";
        }
    }
}
