using UnityEngine;

namespace Software10101.Utils {
	[RequireComponent(typeof(Camera))]
	public sealed class CameraPlanes : MonoBehaviour {
		public static CameraPlanes Instance;
		public Plane[] Planes;
		public float Distance = 10000.0f;

		private Camera thisCamera;

		private void Awake () {
			thisCamera = GetComponent<Camera>();

			Calculate();

			if (thisCamera == Camera.main) {
				Instance = this;
			}
		}

		void LateUpdate () {
			Calculate();
		}

		private void Calculate () {
			Planes = GeometryUtility.CalculateFrustumPlanes(GetComponent<Camera>());

			Planes[5].distance = Distance;
		}
	}
}
