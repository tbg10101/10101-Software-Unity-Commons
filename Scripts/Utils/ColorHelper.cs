using UnityEngine;
using System.Collections.Generic;

namespace Software10101.Utils {
    public static class ColorHelper {
        public static readonly Color Cyan;
        public static readonly Color CyanTransparent;
        public static readonly Color CyanDark;
        public static readonly Color Green;
        public static readonly Color GreenTransparent;
        public static readonly Color GreenDark;
        public static readonly Color Blue;
        public static readonly Color BlueTransparent;
        public static readonly Color BlueDark;
        public static readonly Color Purple;
        public static readonly Color PurpleTransparent;
        public static readonly Color PurpleDark;
        public static readonly Color Yellow;
        public static readonly Color YellowTransparent;
        public static readonly Color YellowDark;
        public static readonly Color Orange;
        public static readonly Color OrangeTransparent;
        public static readonly Color OrangeDark;
        public static readonly Color Red;
        public static readonly Color RedTransparent;
        public static readonly Color RedDark;
        public static readonly Color White;
        public static readonly Color WhiteTransparent;
        public static readonly Color WhiteDark;
        public static readonly Color Black;
        public static readonly Color BlackTransparent;
        public static readonly Color BlackDark;
        public static readonly Color Gray;
        public static readonly Color GrayTransparent;
        public static readonly Color GrayDark;

        public static readonly Color StandardDarkening = new Color32(236 - 189, 240 - 195, 241 - 199, 255);
        public static readonly Color StandardTransparency = new Color(0.0f, 0.0f, 0.0f, 0.875f);

        public static readonly Dictionary<Color, Color> DarkenedColors = new Dictionary<Color, Color>();
        public static readonly Dictionary<Color, Color> TransparentColors = new Dictionary<Color, Color>();

        static ColorHelper () {
            // colors from http://flatuicolors.com/

            ColorUtility.TryParseHtmlString("#1abc9c", out Cyan);
            ColorUtility.TryParseHtmlString("#1abc9c1f", out CyanTransparent);
            ColorUtility.TryParseHtmlString("#16a085", out CyanDark);
            ColorUtility.TryParseHtmlString("#2ecc71", out Green);
            ColorUtility.TryParseHtmlString("#2ecc711f", out GreenTransparent);
            ColorUtility.TryParseHtmlString("#27ae60", out GreenDark);
            ColorUtility.TryParseHtmlString("#3498db", out Blue);
            ColorUtility.TryParseHtmlString("#3498db1f", out BlueTransparent);
            ColorUtility.TryParseHtmlString("#2980b9", out BlueDark);
            ColorUtility.TryParseHtmlString("#9b59b6", out Purple);
            ColorUtility.TryParseHtmlString("#9b59b61f", out PurpleTransparent);
            ColorUtility.TryParseHtmlString("#8e44ad", out PurpleDark);
            ColorUtility.TryParseHtmlString("#f1c40f", out Yellow);
            ColorUtility.TryParseHtmlString("#f1c40f1f", out YellowTransparent);
            ColorUtility.TryParseHtmlString("#f39c12", out YellowDark);
            ColorUtility.TryParseHtmlString("#e67e22", out Orange);
            ColorUtility.TryParseHtmlString("#e67e221f", out OrangeTransparent);
            ColorUtility.TryParseHtmlString("#d35400", out OrangeDark);
            ColorUtility.TryParseHtmlString("#e74c3c", out Red);
            ColorUtility.TryParseHtmlString("#e74c3c1f", out RedTransparent);
            ColorUtility.TryParseHtmlString("#c0392b", out RedDark);
            ColorUtility.TryParseHtmlString("#ecf0f1", out White);
            ColorUtility.TryParseHtmlString("#ecf0f11f", out WhiteTransparent);
            ColorUtility.TryParseHtmlString("#bdc3c7", out WhiteDark);
            ColorUtility.TryParseHtmlString("#34495e", out Black);
            ColorUtility.TryParseHtmlString("#34495e1f", out BlackTransparent);
            ColorUtility.TryParseHtmlString("#2c3e50", out BlackDark);
            ColorUtility.TryParseHtmlString("#95a5a6", out Gray);
            ColorUtility.TryParseHtmlString("#95a5a61f", out GrayTransparent);
            ColorUtility.TryParseHtmlString("#7f8c8d", out GrayDark);

            DarkenedColors.Add(Cyan, CyanDark);
            DarkenedColors.Add(Green, GreenDark);
            DarkenedColors.Add(Blue, BlueDark);
            DarkenedColors.Add(Purple, PurpleDark);
            DarkenedColors.Add(Yellow, YellowDark);
            DarkenedColors.Add(Orange, OrangeDark);
            DarkenedColors.Add(Red, RedDark);
            DarkenedColors.Add(White, WhiteDark);
            DarkenedColors.Add(Black, BlackDark);
            DarkenedColors.Add(Gray, GrayDark);

            TransparentColors.Add(Cyan, CyanTransparent);
            TransparentColors.Add(Green, GreenTransparent);
            TransparentColors.Add(Blue, BlueTransparent);
            TransparentColors.Add(Purple, PurpleTransparent);
            TransparentColors.Add(Yellow, YellowTransparent);
            TransparentColors.Add(Orange, OrangeTransparent);
            TransparentColors.Add(Red, RedTransparent);
            TransparentColors.Add(White, WhiteTransparent);
            TransparentColors.Add(Black, BlackTransparent);
            TransparentColors.Add(Gray, GrayTransparent);
        }

        public static Color MultiplyAlpha (Color inColor, float alphaMultiplier) {
            return new Color(inColor.r, inColor.g, inColor.b, inColor.a * alphaMultiplier);
        }

        public static Color GetColorPhysical (float tInput) {
            return GetColorPhysical((double)tInput);
        }

        /*
         * http://en.wikipedia.org/wiki/Color_temperature
         * The returned color's alpha is the temperature as a fraction of the maximum temperature given.
         */
        public static Color GetColorPhysical (double tInput) {
            Color o = Color.white;

            float t = Mathf.Clamp((float)tInput, 1000.0f, 40000.0f);

            o.a = t / 40000.0f;

            //All calculations require Kelvin/100, so only do the conversion once
            t = (float)(tInput / 100.0f);

            double x;
            double y;
            double z;

            //red
            if (t <= 66.0) {
                x = 255.0;
            } else {
                x = t - 60.0;
                x = 329.698727446 * System.Math.Pow(x, -0.1332047592);
            }

            x = System.Math.Min(255.0, System.Math.Max(0.0, x));

            //green
            if (t <= 66.0) {
                y = t;
                y = 99.4708025861 * System.Math.Log(y) - 161.1195681661;
            } else {
                y = t - 60.0;
                y = 288.1221695283 * System.Math.Pow(y, -0.0755148492);
            }

            y = System.Math.Min(255.0, System.Math.Max(0.0, y));

            //blue
            if (t >= 66.0) {
                z = 255.0;
            } else if (t < 19.0f) {
                z = 0.0;
            } else {
                z = t - 10.0;
                z = 138.5177312231 * System.Math.Log(z) - 305.0447927307;
            }

            z = System.Math.Min(255.0, System.Math.Max(0.0, z));

            // convert to color
            o.r = (float)(x / 255.0);
            o.g = (float)(y / 255.0);
            o.b = (float)(z / 255.0);

            return o;
        }

        public static Color Pow (this Color c, float exponent) {
            return new Color(Mathf.Pow(
                c.r, exponent),
                Mathf.Pow(c.g, exponent),
                Mathf.Pow(c.b, exponent),
                c.a);
        }
    }
}
