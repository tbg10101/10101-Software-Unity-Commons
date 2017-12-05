using UnityEngine;

namespace Software10101.Utils {
	public sealed class DontDestroyOnLoad : MonoBehaviour {
		private void Start () {
			DontDestroyOnLoad(gameObject);
		}
	}
}
