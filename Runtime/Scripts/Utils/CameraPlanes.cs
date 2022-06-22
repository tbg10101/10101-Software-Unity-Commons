using UnityEngine;

namespace Software10101.Utils {
    [RequireComponent(typeof(Camera))]
    public sealed class CameraPlanes : MonoBehaviour {
        public static CameraPlanes Instance;
        public Plane[] Planes;
        public float Distance = 10000.0f;

        private Camera _camera;

        private void Awake () {
            _camera = GetComponent<Camera>();

            Calculate();

            if (_camera == Camera.main) {
                Instance = this;
            }
        }

        private void LateUpdate () {
            Calculate();
        }

        private void Calculate () {
            Planes = GeometryUtility.CalculateFrustumPlanes(GetComponent<Camera>());

            Planes[5].distance = Distance;
        }
    }
}
