using UnityEngine;

namespace Software10101.Components {
	public sealed class DestroyOnLoad : MonoBehaviour {
		private void Start () {
			Destroy(gameObject);
		}
	}
}
