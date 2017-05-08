using UnityEngine;

namespace Software10101.Utils {
	public sealed class LookAtCamera : MonoBehaviour {
		private void Update () {
			transform.LookAt(Camera.main.transform);
		}
	}
}
