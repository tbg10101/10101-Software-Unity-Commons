using UnityEngine;

namespace Software10101.Utils.VR {
	/// <summary>
	/// Since Unity does not allow you to set the position of VR cameras, this is a workaround. 
	/// 
	/// Add the camera that you want to lock as child of the object to which this script is attached.
	/// 
	/// Rotation is not affected.
	/// 
	/// An example of a use for this is rendering objects that are effectively an infinite distance away, such as skybox objects.
	/// </summary>
	public sealed class LockVrCameraPosition : MonoBehaviour {
		public Camera cam;

		private void LateUpdate () {
			transform.localPosition = -cam.transform.localPosition;
		}
	}
}
