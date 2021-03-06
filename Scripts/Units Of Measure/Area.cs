﻿namespace Software10101.Units {
	public struct Area {
		private const string UNIT = "km²";
		
		public static readonly Area ZERO_AREA =        0.0; // km²
		public static readonly Area SQUARE_KILOMETER = 1.0; // km²
		public static readonly Area MAX_AREA = double.MaxValue;

		private readonly double kmSquared;

		/////////////////////////////////////////////////////////////////////////////
		// BOXING
		/////////////////////////////////////////////////////////////////////////////
		public Area (double a) {
			kmSquared = a;
		}

		public Area (double a, Area unit) {
			kmSquared = a * unit.kmSquared;
		}

		public static Area From (double a) {
			return new Area(a);
		}

		public static Area From (double a, Area unit) {
			return new Area(a, unit);
		}

		public static implicit operator Area (double a) {
			return From(a);
		}

		/////////////////////////////////////////////////////////////////////////////
		// UN-BOXING
		/////////////////////////////////////////////////////////////////////////////
		public double To (Area unit) {
			return kmSquared / unit;
		}

		public static implicit operator double (Area a) {
			return a.kmSquared;
		}

		/////////////////////////////////////////////////////////////////////////////
		// OPERATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Area operator + (Area first, Area second) {
			return first.kmSquared + second.kmSquared;
		}

		public static Area operator + (Area first, double second) {
			return first.kmSquared + second;
		}

		public static Area operator + (double first, Area second) {
			return first + second.kmSquared;
		}

		public static Area operator - (Area first, Area second) {
			return first.kmSquared - second.kmSquared;
		}

		public static Area operator - (Area first, double second) {
			return first.kmSquared - second;
		}

		public static Area operator - (double first, Area second) {
			return first - second.kmSquared;
		}

		public static Area operator * (Area first, double second) {
			return first.kmSquared * second;
		}

		public static Area operator * (double first, Area second) {
			return first * second.kmSquared;
		}

		public static double operator / (Area first, Area second) {
			return first.kmSquared / second.kmSquared;
		}

		public static Area operator / (Area first, double second) {
			return first.kmSquared / second;
		}

		/////////////////////////////////////////////////////////////////////////////
		// MUTATORS
		/////////////////////////////////////////////////////////////////////////////
		public static Length operator / (Area area, Length length) {
			return area.kmSquared / length.To(Length.KILOMETER);
		}

		public static Volume operator * (Area first, Length second) {
			return first.kmSquared * second.To(Length.KILOMETER);
		}

		/////////////////////////////////////////////////////////////////////////////
		// TO STRING
		/////////////////////////////////////////////////////////////////////////////
		override
		public string ToString () {
			return ToStringSquareKilometers();
		}

		public string ToStringSquareKilometers () {
			return string.Format("{0:F2}{1}", To(SQUARE_KILOMETER), UNIT);
		}
	}
}
