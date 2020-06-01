using UnityEngine;

namespace Software10101.Components {
    public sealed class DisableOnAwake : MonoBehaviour {
        private void Awake () {
            gameObject.SetActive(false);
        }
    }
}
