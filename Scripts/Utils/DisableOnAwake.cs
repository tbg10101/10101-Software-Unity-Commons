using UnityEngine;

namespace Software10101.Utils {
	public sealed class DisableOnAwake : MonoBehaviour {
		private void Awake () {
			gameObject.SetActive(false);
		}
	}
}
