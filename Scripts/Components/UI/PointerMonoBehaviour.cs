using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Software10101.Components.UI {
    public class PointerMonoBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField]
        private UnityEvent _onPointerEnter = null;

        [SerializeField]
        private UnityEvent _onPointerExit = null;

        public void OnPointerEnter(PointerEventData eventData) {
            _onPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData) {
            _onPointerExit?.Invoke();
        }
    }
}
