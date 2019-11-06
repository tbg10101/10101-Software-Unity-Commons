using UnityEngine;

namespace Software10101.Components.UI {
    [RequireComponent(typeof(RectTransform))]
    public class UiAnchor : MonoBehaviour {
        public Transform Anchor;
        public Vector3 WorldOffset;
        public Vector2 UiOffset;

        public bool CameraSpace;

        public Camera Camera;
        private Canvas _canvas;

        private void Start() {
            Transform t = transform;

            while (!t.TryGetComponent(out _canvas)) {
                t = t.parent;
            }
        }

        private void LateUpdate() {
            ApplyPosition();
        }

        public void ApplyPosition() {
            if (!Anchor) {
                return;
            }

            if (!Camera) {
                Camera = Camera.main;
            }

            if (CameraSpace) {
                Vector2 canvasSize = ((RectTransform)_canvas.transform).sizeDelta;
                Vector2 canvasOffset = canvasSize / 2.0f;
                Vector2 viewportPoint = Camera.WorldToViewportPoint(Anchor.position + WorldOffset);
                Vector2 canvasPoint = Vector2.Scale(viewportPoint, canvasSize) + UiOffset - canvasOffset;

                transform.localPosition = canvasPoint;
            } else {
                transform.position = RectTransformUtility.WorldToScreenPoint(Camera, Anchor.position + WorldOffset) + UiOffset;
            }
        }
    }
}
