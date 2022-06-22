using System;
using UnityEngine;
using UnityEngine.UI;

namespace Software10101.Components.UI {
    [ExecuteAlways]
    public class WedgeProgressBar : MonoBehaviour {
        public Color Color;
        public Graphic[] Wedges;

        [Range(0, 1)]
        public float Fraction;

        public bool FadeWedges = true;

        private void Update() {
            float fraction = Mathf.Max(0, Mathf.Min(1, Fraction));

            int maxWedge = (int)(Wedges.Length * fraction);

            for (int i = 0; i < Wedges.Length; i++) {
                try {
                    Wedges[i].color = i < maxWedge ? new Color(Color.r, Color.g, Color.b, Color.a) : Color.clear;

                    if (FadeWedges && i == maxWedge) {
                        float min = (float)maxWedge / Wedges.Length;
                        float max = (float)(maxWedge + 1) / Wedges.Length;

                        float offsetFraction = fraction - min;
                        float offsetMax = max - min;

                        Wedges[i].color = new Color(Color.r, Color.g, Color.b, Color.a * (offsetFraction/ offsetMax));
                    }
                } catch (IndexOutOfRangeException) {
                    Debug.LogError("Wedge index out of range: " + i);
                }
            }
        }
    }
}
