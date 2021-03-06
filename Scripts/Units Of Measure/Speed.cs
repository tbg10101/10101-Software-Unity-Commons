﻿namespace Software10101.Units {
	public struct Speed {
		private const string UNIT = "m/s";
		
		public static readonly Speed ZERO_SPEED       = 0.0; // m/s
		public static readonly Speed METER_PER_SECOND = 1.0; // m/s
		public static readonly Speed C =        299792458.0; // m/s
		public static readonly Speed MAX_SPEED = double.MaxValue;

		public Length length;
		public Duration duration;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Speed (double s) {
			length = Length.From(s, Length.METER);
			duration = Duration.SECOND;
		}

		public Speed (Length l, Duration d) {
			length = l;
			duration = d;
		}

		public static Speed From (double s) {
			return new Speed(s);
		}

		public static Speed From (Length l, Duration d) {
			return new Speed(l, d);
		}

		public static implicit operator Speed (double s) {
			return From(s);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Length lUnit, Duration dUnit) {
			return length.To(lUnit) / duration.To(dUnit);
		}

		public static implicit operator double (Speed s) {
			return s.length.To(Length.METER) / s.duration.To(Duration.SECOND);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Speed operator + (Speed first, Speed second) {
			return first.To(Length.METER, Duration.SECOND) + second.To(Length.METER, Duration.SECOND);
		}

		public static Speed operator + (Speed first, double second) {
			return first.To(Length.METER, Duration.SECOND) + second;
		}

		public static Speed operator + (double first, Speed second) {
			return first + second.To(Length.METER, Duration.SECOND);
		}

		public static Speed operator - (Speed first, Speed second) {
			return first.To(Length.METER, Duration.SECOND) - second.To(Length.METER, Duration.SECOND);
		}

		public static Speed operator - (Speed first, double second) {
			return first.To(Length.METER, Duration.SECOND) - second;
		}

		public static Speed operator - (double first, Speed second) {
			return first - second.To(Length.METER, Duration.SECOND);
		}

		public static Speed operator * (Speed first, double second) {
			return first.To(Length.METER, Duration.SECOND) * second;
		}

		public static Speed operator * (double first, Speed second) {
			return first * second.To(Length.METER, Duration.SECOND);
		}

		public static double operator / (Speed first, Speed second) {
			return first.To(Length.METER, Duration.SECOND) / second.To(Length.METER, Duration.SECOND);
		}

		public static Speed operator / (Speed first, double second) {
			return first.To(Length.METER, Duration.SECOND) / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator * (Speed speed, Duration duration) {
			return speed.length * (duration / speed.duration);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		override
		public string ToString () {
			return ToStringMetersPerSecond();
		}

		public string ToStringMetersPerSecond () {
			return string.Format("{0:F2}{1}", To(Length.METER, Duration.SECOND), UNIT);
		}

		public string ToStringKilometersPerHour () {
			return string.Format("{0:F2}{1}", To(Length.KILOMETER, Duration.HOUR), "km/h");
		}
	}
}
