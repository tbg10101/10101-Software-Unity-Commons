using System;
using UnityEngine;
using UnityEngine.UI;

namespace Software10101.Components.UI {
    [ExecuteAlways]
    public class UiSpinner : MonoBehaviour {
        public Color Color;
        public Graphic[] Petals;

        public float Speed = 360.0f;

        private float _preciseAngle = 0.0f;

        private void Update() {
            _preciseAngle -= Speed * Time.unscaledDeltaTime;

            while (_preciseAngle >= 360.0f) {
                _preciseAngle -= 360.0f;
            }

            while (_preciseAngle < 0.0f) {
                _preciseAngle += 360.0f;
            }

            int petalCount = Petals.Length;
            int petalIndex = Mathf.FloorToInt(_preciseAngle / 360.0f * petalCount);
            float opacity = 1.0f;
            float opacityIncrement = 0.9f / petalCount;

            for (int i = 0; i < petalCount; i++) {
                if (petalIndex >= petalCount) {
                    petalIndex = 0;
                }

                try {
                    Petals[petalIndex].color = new Color(Color.r, Color.g, Color.b, opacity * Color.a);
                } catch (IndexOutOfRangeException) {
                    Debug.LogError("Petal Index: " + petalIndex);
                }

                opacity -= opacityIncrement;
                petalIndex++;
            }
        }
    }
}
