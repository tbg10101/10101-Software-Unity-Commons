using UnityEngine;

namespace Software10101.Utils.VR {
	/// <summary>
	/// Gets and maintains a mapping of tracked objects to controllers.
	/// </summary>
	public sealed class VrDevices : MonoBehaviour {
		public static readonly Vector3 ControllerTipOffset = new Vector3(0.0f, -0.0785f, 0.0405f);

		public static VrDevices Instance = null;

		private static SteamVR_Controller.Device _rightControllerDevice = null;
		public static SteamVR_Controller.Device Right {
			get {
				return _rightControllerDevice;
			}
		}

		private static SteamVR_Controller.Device _leftControllerDevice = null;
		public static SteamVR_Controller.Device Left {
			get {
				return _leftControllerDevice;
			}
		}

		private static bool _allDevicesReady = false;
		public static bool AllDevicesReady {
			get {
				return _allDevicesReady;
			}
		}

		private static FrameCache<Vector3> _rightControllerPosition = null;
		public static FrameCache<Vector3> RightControllerPosition {
			get {
				return _rightControllerPosition;
			}
		}

		private static FrameCache<Vector3> _leftControllerPosition = null;
		public static FrameCache<Vector3> LeftControllerPosition {
			get {
				return _leftControllerPosition;
			}
		}

		private static FrameCache<Vector3> _rightControllerTipPosition = null;
		public static FrameCache<Vector3> RightControllerTipPosition {
			get {
				return _rightControllerTipPosition;
			}
		}
		
		private static FrameCache<Vector3> _leftControllerTipPosition = null;
		public static FrameCache<Vector3> LeftControllerTipPosition {
			get {
				return _leftControllerTipPosition;
			}
		}

		public SteamVR_TrackedObject RightControllerObject;
		public SteamVR_TrackedObject LeftControllerObject;

		private void Awake () {
			if (Instance == null) {
				DontDestroyOnLoad(gameObject);
				Instance = this;
				
				_rightControllerPosition = new FrameCache<Vector3>(GetRightControllerPosition);
				_leftControllerPosition = new FrameCache<Vector3>(GetLeftControllerPosition);
				
				_rightControllerTipPosition = new FrameCache<Vector3>(GetRightControllerTipPosition);
				_leftControllerTipPosition = new FrameCache<Vector3>(GetLeftControllerTipPosition);
			} else {
				DestroyImmediate(gameObject);
			}
		}

		private void Update () {
			_rightControllerDevice = SteamVR_Controller.Input((int)RightControllerObject.index);
			_leftControllerDevice = SteamVR_Controller.Input((int)LeftControllerObject.index);

			_allDevicesReady = _rightControllerDevice != null && _leftControllerDevice != null;
		}

		public Vector3 GetRightControllerPosition () {
			return _allDevicesReady ? RightControllerObject.transform.position : Vector3.zero;
		}

		public Vector3 GetLeftControllerPosition () {
			return _allDevicesReady ? LeftControllerObject.transform.position : Vector3.zero;
		}

		public Vector3 GetRightControllerTipPosition () {
			return _allDevicesReady ? RightControllerObject.transform.position + ControllerTipOffset : Vector3.zero;
		}

		public Vector3 GetLeftControllerTipPosition () {
			return _allDevicesReady ? LeftControllerObject.transform.position + ControllerTipOffset : Vector3.zero;
		}
	}
}
