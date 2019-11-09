using UnityEngine;

namespace Software10101.Components.UI {
    public abstract class CanvasMonoBehaviour : MonoBehaviour {
        private Canvas _canvas;

        protected Canvas Canvas {
            get {
                if (!_canvas) {
                    Transform t = transform;

                    while (!t.TryGetComponent(out _canvas)) {
                        t = t.parent;
                    }
                }

                return _canvas;
            }
        }
    }
}
