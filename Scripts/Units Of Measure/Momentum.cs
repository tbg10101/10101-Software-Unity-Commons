namespace Software10101.Units {
	public struct Momentum {
		public readonly Mass mass;
		public readonly Speed speed;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Momentum (double d) {
			mass = Mass.From(d, Mass.GRAM);
			speed = Speed.METER_PER_SECOND;
		}

		public Momentum (Mass m, Speed s) {
			mass = m;
			speed = s;
		}

		public static Momentum From (double p) {
			return new Momentum(p);
		}

		public static Momentum From (Mass m, Speed s) {
			return new Momentum(m, s);
		}

		public static implicit operator Momentum (double p) {
			return From(p);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Mass mUnit, Speed sUnit) {
			return mass.To(mUnit) / speed.To(sUnit.length, sUnit.duration);
		}

		public static implicit operator double (Momentum p) {
			return p.mass.To(Mass.KILOGRAM) * p.speed.To(Speed.METER_PER_SECOND.length, Speed.METER_PER_SECOND.duration);
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Momentum operator * (Momentum first, double second) {
			return new Momentum(first.mass * second, first.speed);
		}

		public static Momentum operator * (double first, Momentum second) {
			return new Momentum(second.mass * first, second.speed);
		}

		public static double operator / (Momentum first, Momentum second) {
			return first.To(Mass.KILOGRAM, Speed.METER_PER_SECOND) / second.To(Mass.KILOGRAM, Speed.METER_PER_SECOND);
		}

		public static Momentum operator / (Momentum first, double second) {
			return new Momentum(first.mass / second, first.speed);
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Mass operator / (Momentum momentum, Speed speed) {
			return momentum.mass * (momentum.speed / speed);
		}

		public static Speed operator / (Momentum momentum, Mass mass) {
			return new Speed(momentum.speed * (momentum.mass / mass));
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		public string ToStringKilogramMetersPerSecond () {
			return To(Mass.KILOGRAM, Speed.METER_PER_SECOND) + "kg m/s";
		}
	}
}
