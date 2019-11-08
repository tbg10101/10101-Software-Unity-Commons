using UnityEngine;

namespace Software10101.Components.UI {
    [RequireComponent(typeof(RectTransform))]
    public class UiAnchor : MonoBehaviour {
        public Transform Anchor;
        public Vector3 WorldOffset;
        public Vector2 UiOffset;

        private Camera _camera;
        private Canvas _canvas;

        private void Awake() {
            _camera = Camera.main;
        }

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

            if (_canvas.renderMode != RenderMode.ScreenSpaceOverlay) {
                Vector2 canvasSize = ((RectTransform)_canvas.transform).sizeDelta;
                Vector2 canvasOffset = canvasSize / 2.0f;
                Vector2 viewportPoint = _canvas.worldCamera.WorldToViewportPoint(Anchor.position + WorldOffset);
                Vector2 canvasPoint = Vector2.Scale(viewportPoint, canvasSize) + UiOffset - canvasOffset;

                transform.localPosition = canvasPoint;
            } else {
                transform.position = RectTransformUtility.WorldToScreenPoint(_camera, Anchor.position + WorldOffset) + UiOffset;
            }
        }
    }
}
