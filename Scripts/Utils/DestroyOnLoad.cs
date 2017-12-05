using UnityEngine;

namespace Software10101.Utils {
	public sealed class DestroyOnLoad : MonoBehaviour {
		private void Start () {
			Destroy(gameObject);
		}
	}
}
