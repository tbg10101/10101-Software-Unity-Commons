namespace Software10101.Units {
	public struct VolumetricFlowRate {
		private const string UNIT = "m³/s";

		public static readonly VolumetricFlowRate ZERO_RATE               = 0.0; // m³/s
		public static readonly VolumetricFlowRate METERS_CUBED_PER_SECOND = 1.0; // m³/s

		public Volume volume;
		public Duration duration;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public VolumetricFlowRate (double q) {
			volume = Volume.From(q, Volume.CUBIC_METER);
			duration = Duration.SECOND;
		}

		public VolumetricFlowRate (Volume v, Duration d) {
			volume = v;
			duration = d;
		}

		public static VolumetricFlowRate From (double q) {
			return new VolumetricFlowRate(1);
		}

		public static VolumetricFlowRate From (Volume v, Duration d) {
			return new VolumetricFlowRate(v, d);
		}

		public static implicit operator VolumetricFlowRate (double q) {
			return From(q);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Volume vUnit, Duration dUnit) {
			return volume.To(vUnit) / duration.To(dUnit);
		}

		public static implicit operator double (VolumetricFlowRate q) {
			return q.volume.To(Volume.CUBIC_METER) / q.duration.To(Duration.SECOND);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static VolumetricFlowRate operator + (VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(Volume.CUBIC_METER, Duration.SECOND) + second.To(Volume.CUBIC_METER, Duration.SECOND);
		}

		public static VolumetricFlowRate operator + (VolumetricFlowRate first, double second) {
			return first.To(Volume.CUBIC_METER, Duration.SECOND) + second;
		}

		public static VolumetricFlowRate operator + (double first, VolumetricFlowRate second) {
			return first + second.To(Volume.CUBIC_METER, Duration.SECOND);
		}

		public static VolumetricFlowRate operator - (VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(Volume.CUBIC_METER, Duration.SECOND) - second.To(Volume.CUBIC_METER, Duration.SECOND);
		}

		public static VolumetricFlowRate operator - (VolumetricFlowRate first, double second) {
			return first.To(Volume.CUBIC_METER, Duration.SECOND) - second;
		}

		public static VolumetricFlowRate operator - (double first, VolumetricFlowRate second) {
			return first - second.To(Volume.CUBIC_METER, Duration.SECOND);
		}

		public static VolumetricFlowRate operator * (VolumetricFlowRate first, double second) {
			return first.To(Volume.CUBIC_METER, Duration.SECOND) * second;
		}

		public static VolumetricFlowRate operator * (double first, VolumetricFlowRate second) {
			return first * second.To(Volume.CUBIC_METER, Duration.SECOND);
		}

		public static double operator / (VolumetricFlowRate first, VolumetricFlowRate second) {
			return first.To(Volume.CUBIC_METER, Duration.SECOND) / second.To(Volume.CUBIC_METER, Duration.SECOND);
		}

		public static VolumetricFlowRate operator / (VolumetricFlowRate first, double second) {
			return first.To(Volume.CUBIC_METER, Duration.SECOND) / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Volume operator * (VolumetricFlowRate volumetricFlowRate, Duration duration) {
			return volumetricFlowRate.volume * (duration / volumetricFlowRate.duration);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		override
		public string ToString () {
			return ToStringMetersCubedPerSecond();
		}

		public string ToStringMetersCubedPerSecond () {
			return string.Format("{0:F2}{1}", To(Volume.CUBIC_METER, Duration.SECOND), UNIT);
		}
	}
}
