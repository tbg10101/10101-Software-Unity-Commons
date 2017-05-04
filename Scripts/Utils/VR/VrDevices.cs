using UnityEngine;

namespace Software10101.Utils.VR {
	public sealed class VrDevices : MonoBehaviour {
		private static VrDevices instance = null;

		private static SteamVR_Controller.Device _RightControllerDevice = null;
		public static SteamVR_Controller.Device Right {
			get {
				return _RightControllerDevice;
			}
		}

		private static SteamVR_Controller.Device _LeftControllerDevice = null;
		public static SteamVR_Controller.Device Left {
			get {
				return _LeftControllerDevice;
			}
		}

		private static bool _AllDevicesReady = false;
		public static bool AllDevicesReady {
			get {
				return _AllDevicesReady;
			}
		}

		public SteamVR_TrackedObject rightControllerObject;
		public SteamVR_TrackedObject leftControllerObject;

		private void Start () {
			if (instance == null) {
				DontDestroyOnLoad(gameObject);
				instance = this;
			} else {
				DestroyImmediate(gameObject);
			}
		}

		private void Update () {
			_RightControllerDevice = SteamVR_Controller.Input((int)rightControllerObject.index);
			_LeftControllerDevice = SteamVR_Controller.Input((int)leftControllerObject.index);

			_AllDevicesReady = _RightControllerDevice != null && _LeftControllerDevice != null;
		}
	}
}
