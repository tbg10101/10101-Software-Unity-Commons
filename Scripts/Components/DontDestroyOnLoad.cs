using UnityEngine;

namespace Software10101.Components {
	public sealed class DontDestroyOnLoad : MonoBehaviour {
		private void Start () {
			DontDestroyOnLoad(gameObject);
		}
	}
}
