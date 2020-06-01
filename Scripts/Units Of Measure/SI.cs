using System;
using System.Collections.Generic;

namespace Software10101.Units {
    public static class Si {
        private const int MinOrder = -24;
        private const int MaxOrder = 24;

        private static readonly Dictionary<int, string> Prefixes = new Dictionary<int, string> {
            { MinOrder, "y"},
            {      -21, "z"},
            {      -18, "a"},
            {      -15, "f"},
            {      -12, "p"},
            {       -9, "n"},
            {       -6, "μ"},
            {       -3, "m"},
            {        0,  ""},
            {        3, "k"},
            {        6, "M"},
            {        9, "G"},
            {       12, "T"},
            {       15, "P"},
            {       18, "E"},
            {       21, "Z"},
            { MaxOrder, "Y"}
        };

        public static string ToLargestSiString(
            double value,
            string unit,
            int decimalDigits = 2,
            int valueOrder = 0,
            int minOrderRequested = MinOrder,
            int maxOrderRequested = MaxOrder) {

            int minOrderActual = Math.Max(Math.Min(MaxOrder, minOrderRequested), MinOrder);
            int maxOrderActual = Math.Max(Math.Min(MaxOrder, maxOrderRequested), MinOrder);

            int siOrder = minOrderActual;

            double siValue = value * Math.Pow(10, valueOrder - minOrderActual);

            while (siValue >= 1000.0 && siOrder < maxOrderActual) {
                siOrder += 3;
                siValue /= 1000.0;
            }

            string format = $"{{0:F{decimalDigits}}}{{1}}{{2}}";
            return string.Format(format, siValue, Prefixes[siOrder], unit);
        }
    }
}
