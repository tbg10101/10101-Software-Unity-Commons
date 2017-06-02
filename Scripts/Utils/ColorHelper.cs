using UnityEngine;
using System.Collections.Generic;

namespace Software10101.Utils {
	public static class ColorHelper {
		public static readonly Color CYAN;
		public static readonly Color CYAN_TRANSPARENT;
		public static readonly Color CYAN_DARK;
		public static readonly Color GREEN;
		public static readonly Color GREEN_TRANSPARENT;
		public static readonly Color GREEN_DARK;
		public static readonly Color BLUE;
		public static readonly Color BLUE_TRANSPARENT;
		public static readonly Color BLUE_DARK;
		public static readonly Color PURPLE;
		public static readonly Color PURPLE_TRANSPARENT;
		public static readonly Color PURPLE_DARK;
		public static readonly Color YELLOW;
		public static readonly Color YELLOW_TRANSPARENT;
		public static readonly Color YELLOW_DARK;
		public static readonly Color ORANGE;
		public static readonly Color ORANGE_TRANSPARENT;
		public static readonly Color ORANGE_DARK;
		public static readonly Color RED;
		public static readonly Color RED_TRANSPARENT;
		public static readonly Color RED_DARK;
		public static readonly Color WHITE;
		public static readonly Color WHITE_TRANSPARENT;
		public static readonly Color WHITE_DARK;
		public static readonly Color BLACK;
		public static readonly Color BLACK_TRANSPARENT;
		public static readonly Color BLACK_DARK;
		public static readonly Color GRAY;
		public static readonly Color GRAY_TRANSPARENT;
		public static readonly Color GRAY_DARK;

		public static readonly Color STANDARD_DARKENING = new Color32(236 - 189, 240 - 195, 241 - 199, 255);
		public static readonly Color STANDARD_TRANSPARENCY = new Color(0.0f, 0.0f, 0.0f, 0.875f);

		public static readonly Dictionary<Color, Color> DARKENED_COLORS = new Dictionary<Color, Color>();
		public static readonly Dictionary<Color, Color> TRANSPARENT_COLORS = new Dictionary<Color, Color>();

		static ColorHelper () {
			// colors from http://flatuicolors.com/

			ColorUtility.TryParseHtmlString("#1abc9c", out CYAN);
			ColorUtility.TryParseHtmlString("#1abc9c1f", out CYAN_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#16a085", out CYAN_DARK);
			ColorUtility.TryParseHtmlString("#2ecc71", out GREEN);
			ColorUtility.TryParseHtmlString("#2ecc711f", out GREEN_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#27ae60", out GREEN_DARK);
			ColorUtility.TryParseHtmlString("#3498db", out BLUE);
			ColorUtility.TryParseHtmlString("#3498db1f", out BLUE_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#2980b9", out BLUE_DARK);
			ColorUtility.TryParseHtmlString("#9b59b6", out PURPLE);
			ColorUtility.TryParseHtmlString("#9b59b61f", out PURPLE_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#8e44ad", out PURPLE_DARK);
			ColorUtility.TryParseHtmlString("#f1c40f", out YELLOW);
			ColorUtility.TryParseHtmlString("#f1c40f1f", out YELLOW_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#f39c12", out YELLOW_DARK);
			ColorUtility.TryParseHtmlString("#e67e22", out ORANGE);
			ColorUtility.TryParseHtmlString("#e67e221f", out ORANGE_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#d35400", out ORANGE_DARK);
			ColorUtility.TryParseHtmlString("#e74c3c", out RED);
			ColorUtility.TryParseHtmlString("#e74c3c1f", out RED_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#c0392b", out RED_DARK);
			ColorUtility.TryParseHtmlString("#ecf0f1", out WHITE);
			ColorUtility.TryParseHtmlString("#ecf0f11f", out WHITE_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#bdc3c7", out WHITE_DARK);
			ColorUtility.TryParseHtmlString("#34495e", out BLACK);
			ColorUtility.TryParseHtmlString("#34495e1f", out BLACK_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#2c3e50", out BLACK_DARK);
			ColorUtility.TryParseHtmlString("#95a5a6", out GRAY);
			ColorUtility.TryParseHtmlString("#95a5a61f", out GRAY_TRANSPARENT);
			ColorUtility.TryParseHtmlString("#7f8c8d", out GRAY_DARK);

			DARKENED_COLORS.Add(CYAN, CYAN_DARK);
			DARKENED_COLORS.Add(GREEN, GREEN_DARK);
			DARKENED_COLORS.Add(BLUE, BLUE_DARK);
			DARKENED_COLORS.Add(PURPLE, PURPLE_DARK);
			DARKENED_COLORS.Add(YELLOW, YELLOW_DARK);
			DARKENED_COLORS.Add(ORANGE, ORANGE_DARK);
			DARKENED_COLORS.Add(RED, RED_DARK);
			DARKENED_COLORS.Add(WHITE, WHITE_DARK);
			DARKENED_COLORS.Add(BLACK, BLACK_DARK);
			DARKENED_COLORS.Add(GRAY, GRAY_DARK);

			TRANSPARENT_COLORS.Add(CYAN, CYAN_TRANSPARENT);
			TRANSPARENT_COLORS.Add(GREEN, GREEN_TRANSPARENT);
			TRANSPARENT_COLORS.Add(BLUE, BLUE_TRANSPARENT);
			TRANSPARENT_COLORS.Add(PURPLE, PURPLE_TRANSPARENT);
			TRANSPARENT_COLORS.Add(YELLOW, YELLOW_TRANSPARENT);
			TRANSPARENT_COLORS.Add(ORANGE, ORANGE_TRANSPARENT);
			TRANSPARENT_COLORS.Add(RED, RED_TRANSPARENT);
			TRANSPARENT_COLORS.Add(WHITE, WHITE_TRANSPARENT);
			TRANSPARENT_COLORS.Add(BLACK, BLACK_TRANSPARENT);
			TRANSPARENT_COLORS.Add(GRAY, GRAY_TRANSPARENT);
		}

		public static Vector4 ConvertToHsv (Color c) {
			float cMax = Mathf.Max(c.r, c.g, c.b);
			float cMin = Mathf.Min(c.r, c.g, c.b);
			float cDel = cMax - cMin;

			float val = cMax;
			float sat = 1;
			float hue = 0;

			if (cMax != 0) {
				sat = cDel / cMax;
			} else {
				sat = 0;
				hue = 0;
			}

			if (cMax == c.r) {
				hue = (c.g - c.b) / cDel;
			} else if (cMax == c.g) {
				hue = 2 + (c.b - c.r) / cDel;
			} else {
				hue = 4 + (c.r - c.g) / cDel;
			}

			hue /= 6;

			while (hue < 0) {
				hue++;
			}

			return new Vector4(hue, sat, val, c.a);
		}

		public static Color ConvertToRgb (Vector4 c) {
			float red = 0;
			float grn = 0;
			float blu = 0;

			float h = c.x * 6;
			int i = (int)Mathf.Floor(h);
			float f = h - i;
			float p = c.z * (1 - c.y);
			float q = c.z * (1 - c.y * f);
			float t = c.z * (1 - c.y * (1 - f));

			switch (i) {
				case 0:
					red = c.z;
					grn = t;
					blu = p;
					break;
				case 1:
					red = q;
					grn = c.z;
					blu = p;
					break;
				case 2:
					red = p;
					grn = c.z;
					blu = t;
					break;
				case 3:
					red = p;
					grn = q;
					blu = c.z;
					break;
				case 4:
					red = t;
					grn = p;
					blu = c.z;
					break;
				default:
					red = c.z;
					grn = p;
					blu = q;
					break;
			}

			return new Color(red, grn, blu, c.w);
		}

		public static Color MultiplyAlpha (Color inColor, float multAlpha) {
			return new Color(inColor.r, inColor.g, inColor.b, inColor.a * multAlpha);
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

			double t = Mathf.Clamp((float)tInput, 1000.0f, 40000.0f);

			o.a = (float)(t / 40000.0);

			//All calculations require Kelvin/100, so only do the conversion once
			t = tInput / 100.0;

			Vector3d v = new Vector3d(255.0, 255.0, 255.0);

			//red
			if (t <= 66.0) {
				v.x = 255.0;
			} else {
				v.x = t - 60.0;
				v.x = 329.698727446 * System.Math.Pow(v.x, -0.1332047592);
			}

			v.x = System.Math.Min(255.0, System.Math.Max(0.0, v.x));

			//green
			if (t <= 66.0) {
				v.y = t;
				v.y = 99.4708025861 * System.Math.Log(v.y) - 161.1195681661;
			} else {
				v.y = t - 60.0;
				v.y = 288.1221695283 * System.Math.Pow(v.y, -0.0755148492);
			}

			v.y = System.Math.Min(255.0, System.Math.Max(0.0, v.y));

			//blue
			if (t >= 66.0) {
				v.z = 255.0;
			} else if (t < 19.0f) {
				v.z = 0.0;
			} else {
				v.z = t - 10.0;
				v.z = 138.5177312231 * System.Math.Log(v.z) - 305.0447927307;
			}

			v.z = System.Math.Min(255.0, System.Math.Max(0.0, v.z));

			// convert to color
			o.r = (float)(v.x / 255.0);
			o.g = (float)(v.y / 255.0);
			o.b = (float)(v.z / 255.0);

			return o;
		}

		public static Color pow (this Color c, float exponent) {
			return new Color(Mathf.Pow(c.r, exponent), Mathf.Pow(c.g, exponent), Mathf.Pow(c.b, exponent), c.a);
		}
	}
}
