using UnityEngine;

namespace Software10101.Components.UI {
    [RequireComponent(typeof(RectTransform))]
    public class UiAnchor : CanvasMonoBehaviour {
        public Transform Anchor;
        public Vector3 WorldOffset;
        public Vector2 UiOffset;

        private Camera _camera;

        private void Awake() {
            _camera = Camera.main;
        }

        private void LateUpdate() {
            ApplyPosition();
        }

        public void ApplyPosition() {
            if (!Anchor) {
                return;
            }

            if (Canvas.renderMode != RenderMode.ScreenSpaceOverlay) {
                Vector2 canvasSize = ((RectTransform) Canvas.transform).sizeDelta;
                Vector2 canvasOffset = canvasSize / 2.0f;
                Vector2 viewportPoint = Canvas.worldCamera.WorldToViewportPoint(Anchor.position + WorldOffset);
                Vector2 canvasPoint = Vector2.Scale(viewportPoint, canvasSize) + UiOffset - canvasOffset;

                transform.localPosition = canvasPoint;
            } else {
                transform.position = RectTransformUtility.WorldToScreenPoint(_camera, Anchor.position + WorldOffset) + UiOffset;
            }
        }
    }
}
