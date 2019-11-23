using UnityEngine;
using UnityEngine.EventSystems;

namespace Software10101.Components {
    public abstract class MouseOverBehaviour : MonoBehaviour {
        public static MouseOverBehaviour ObjectCurrentlyUnderCursorWithoutUi { get; private set; } = null;

        private bool _mouseOver = false;
        public bool IsMouseOver = false;
        private bool _previousIsMouseOver = false;

        protected virtual void OnMouseOver() {
            _mouseOver = true;
            bool mouseOverUi = EventSystem.current.IsPointerOverGameObject();
            IsMouseOver = _mouseOver && !mouseOverUi;

            if (_previousIsMouseOver && !IsMouseOver) {
                ObjectCurrentlyUnderCursorWithoutUi = null;
                OnMouseExitWithoutUi();
            } else if (!_previousIsMouseOver && IsMouseOver) {
                ObjectCurrentlyUnderCursorWithoutUi = this;
                OnMouseEnterWithoutUi();
            }

            _previousIsMouseOver = IsMouseOver;
        }

#if UNITY_EDITOR
    protected virtual void OnMouseEnter() {
        // stubbed out for development, but don't want to pay performance cost when released
    }
#endif

        protected virtual void OnMouseEnterWithoutUi() { }

        protected virtual void OnMouseExit() {
            _mouseOver = false;
            IsMouseOver = false;

            if (_previousIsMouseOver) {
                ObjectCurrentlyUnderCursorWithoutUi = null;
                OnMouseExitWithoutUi();
            }

            _previousIsMouseOver = false;
        }

        protected virtual void OnMouseExitWithoutUi() { }

        protected virtual void OnMouseDown() {
            if (IsMouseOver) {
                OnMouseDownWithoutUi();
            }
        }

        protected virtual void OnMouseDownWithoutUi() { }

        protected virtual void OnMouseUp() {
            if (IsMouseOver) {
                OnMouseUpWithoutUi();
            }
        }

        protected virtual void OnMouseUpWithoutUi() { }

        protected virtual void OnDisable() {
            OnMouseExit();
        }

        protected virtual void OnDestroy() {
            if (ObjectCurrentlyUnderCursorWithoutUi == this) {
                ObjectCurrentlyUnderCursorWithoutUi = null;
            }
        }
    }
}
