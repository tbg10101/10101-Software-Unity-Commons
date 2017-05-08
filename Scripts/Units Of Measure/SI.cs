using System;
using System.Collections.Generic;

namespace Software10101.Units {
	public static class SI {
		private const int MIN_ORDER = -24;
		private const int MAX_ORDER = 24;

		private static readonly Dictionary<int, string> PREFIXES = new Dictionary<int, string>() {
			{ MIN_ORDER, "y"},
			{       -21, "z"},
			{       -18, "a"},
			{       -15, "f"},
			{       -12, "p"},
			{        -9, "n"},
			{        -6, "μ"},
			{        -3, "m"},
			{         0,  ""},
			{         3, "k"},
			{         6, "M"},
			{         9, "G"},
			{        12, "T"},
			{        15, "P"},
			{        18, "E"},
			{        21, "Z"},
			{ MAX_ORDER, "Y"}
		};

		public static string ToLargestSiString (
			double value, string unit, int decimalDigits = 2, int valueOrder = 0, int minOrderRequested = MIN_ORDER, int maxOrderRequested = MAX_ORDER) {

			int minOrderActual = Math.Max(Math.Min(MAX_ORDER, minOrderRequested), MIN_ORDER);
			int maxOrderActual = Math.Max(Math.Min(MAX_ORDER, maxOrderRequested), MIN_ORDER);

			int siOrder = minOrderActual;

			double siValue = value * Math.Pow(10, valueOrder - minOrderActual);

			while (siValue >= 1000.0 && siOrder < maxOrderActual) {
				siOrder += 3;
				siValue /= 1000.0;
			}

			return string.Format("{0:F2}{1}{2}", siValue, PREFIXES[siOrder], unit);
		}
	}
}