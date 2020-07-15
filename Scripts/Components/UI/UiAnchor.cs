using UnityEngine;

namespace Software10101.Components.UI {
    [RequireComponent(typeof(RectTransform))]
    public class UiAnchor : CanvasMonoBehaviour {
        public Transform Anchor;
        public Vector3 WorldOffset;
        public Vector2 UiOffset;

        public Camera Camera;

        private void Awake() {
            if (Camera == null) {
                Camera = Camera.main;
            }
        }

        private void LateUpdate() {
            ApplyPosition();
        }

        public void ApplyPosition() {
            if (!Anchor) {
                return;
            }

            Vector2 canvasSize = ((RectTransform) Canvas.transform).sizeDelta;
            Vector2 canvasOffset = canvasSize / 2.0f;
            Vector2 viewportPoint = Camera.WorldToViewportPoint(Anchor.position + WorldOffset);
            Vector2 canvasPoint = Vector2.Scale(viewportPoint, canvasSize) + UiOffset - canvasOffset;

            transform.localPosition = canvasPoint;
        }
    }
}
