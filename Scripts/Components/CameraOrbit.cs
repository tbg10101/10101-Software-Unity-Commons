using UnityEngine;

namespace Software10101.Components {
	public sealed class CameraOrbit : MonoBehaviour {
		public static CameraOrbit instance;

		public Vector3 focusPoint;
		public Vector3 cameraWorldPosition;
		public bool worldRelative;

		public float distance = 50.0f;
		public float minDistance = 2.5f;
		public float maxDistance = 1000.0f;

		public float moveSpeed = 1.0f;

		public float zoomRate = 50.0f;

		private float x = 0.0f;
		private float y = 0.0f;

		public float xSpeed = 250.0f;
		public float ySpeed = 120.0f;

		private const float inFOV = 5.0f;
		private const float outFOV = 60.0f;
		private float toFOV = outFOV;
		private float timeFrom = -1.0f;
		private float switchedFOV = outFOV;
		public float punchInTime = 0.2f;
		public Camera[] fovAffectedCameras;
		private float lastPunchInValue = 0.0f;

		public Camera mainCamera = null;

		private void Awake () {
			instance = this;
		}

		private void Start () {
			if (mainCamera == null) {
				mainCamera = Camera.main;
			}
		}

		private void LateUpdate () {
			Vector3 planarDirection = new Vector3(transform.forward.x, 0.0f, transform.forward.z).normalized;

			// forward and back
			focusPoint += planarDirection * (Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed * distance);

			//left and right
			planarDirection.y = planarDirection.x;
			planarDirection.x = planarDirection.z;
			planarDirection.z = -planarDirection.y;
			planarDirection.y = 0.0f;

			int flippedMod = 1;

			if (y > 90.0f && y < 270.0f) {
				flippedMod = -1;
			}

			focusPoint += flippedMod * planarDirection * (Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed * distance);

			// up and down
			focusPoint += new Vector3(0.0f, flippedMod * Input.GetAxis("Through") * Time.deltaTime * moveSpeed * distance, 0.0f);

			// orbit
			if (Input.GetAxisRaw("Select") == 0.0f && (Input.GetAxisRaw("Rotate Camera") > 0.0f)) {
				x += flippedMod * Input.GetAxis("Mouse X") * xSpeed * 0.02f * (mainCamera.fieldOfView / outFOV);
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f * (mainCamera.fieldOfView / outFOV);

				x = ClampAngle(x);
				y = ClampAngle(y);
			}

			// distance (zoom)
			distance -= (Input.GetAxis("Zoom") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
			if (distance < minDistance) {
				distance = minDistance;
			}

			if (distance > maxDistance) {
				distance = maxDistance;
			}

			Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
			Vector3 position = (rotation * new Vector3(0.0f, 0.0f, -distance)) + focusPoint;

			transform.rotation = rotation;
			transform.position = position;

			// punch-in
			if (lastPunchInValue == 0.0f && Input.GetAxisRaw("Punch In") > 0.0f) {
				toFOV = (toFOV == inFOV ? outFOV : inFOV);

				timeFrom = 0.0f;
				switchedFOV = mainCamera.fieldOfView;

				if (toFOV == inFOV) {
					Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
					focusPoint = ray.origin + ray.direction * distance;
					mainCamera.transform.LookAt(focusPoint, flippedMod * Vector3.up);
					y = mainCamera.transform.rotation.eulerAngles.x;
					x = mainCamera.transform.rotation.eulerAngles.y;
				}
			}

			if (timeFrom >= 0.0f) {
				timeFrom += Time.unscaledDeltaTime;

				float nowFOV = Mathf.SmoothStep(switchedFOV, toFOV, timeFrom / punchInTime);

				foreach (Camera c in fovAffectedCameras) {
					c.fieldOfView = nowFOV;
				}
			}

			if (timeFrom >= punchInTime) {
				timeFrom = -1.0f;

				foreach (Camera c in fovAffectedCameras) {
					c.fieldOfView = toFOV;
				}
			}

			lastPunchInValue = Input.GetAxisRaw("Punch In");

			if (worldRelative) {
				focusPoint -= position;
				cameraWorldPosition -= position;
				transform.position = Vector3.zero;
			}

#if UNITY_EDITOR
			Debug.DrawLine(focusPoint + Vector3.left, focusPoint + Vector3.right, Color.red);
			Debug.DrawLine(focusPoint + Vector3.forward, focusPoint + Vector3.back, Color.blue);
			Debug.DrawLine(focusPoint + Vector3.up, focusPoint + Vector3.down, Color.green);
#endif
		}

		private static float ClampAngle (float angle) {
			if (angle < 0.0f)
				return angle + 360.0f;
			if (angle > 360.0f)
				return angle - 360.0f;

			return angle;
		}
	}
}
