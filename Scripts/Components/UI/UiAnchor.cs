using UnityEngine;

namespace Software10101.Components.UI {
    [RequireComponent(typeof(RectTransform))]
    public class UiAnchor : MonoBehaviour {
        public Transform Anchor;
        public Vector3 WorldOffset;
        public Vector2 UiOffset;

        private Camera _camera;

        private void Start() {
            _camera = Camera.main;
        }

        private void LateUpdate() {
            if (!Anchor) {
                return;
            }

            transform.position = RectTransformUtility.WorldToScreenPoint(_camera, Anchor.position + WorldOffset) + UiOffset;
        }
    }
}
