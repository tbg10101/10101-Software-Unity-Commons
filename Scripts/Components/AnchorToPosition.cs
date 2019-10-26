using UnityEngine;

namespace Software10101.Components {
    [ExecuteAlways]
    public class AnchorToPosition : MonoBehaviour {
        public Vector3 Position;

        private void LateUpdate() {
            transform.position = Position;
        }
    }
}
