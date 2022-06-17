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
#if UNITY_2021_3_OR_NEWER
            if (!eventData.fullyExited) {
                return;
            }
#endif

            _onPointerExit?.Invoke();
        }
    }
}
