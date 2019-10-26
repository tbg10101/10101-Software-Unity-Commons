using UnityEngine;

namespace Software10101.Components {
    public class ReplicateRotation : MonoBehaviour {
        public Transform Source;

        private void LateUpdate() {
            if (Source) {
                transform.rotation = Source.rotation;
            } else {
                Debug.LogWarningFormat("No source for rotation replication for object: {0}", name);
            }
        }
    }
}
