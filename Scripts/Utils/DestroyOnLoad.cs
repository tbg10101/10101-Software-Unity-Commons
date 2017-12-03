using UnityEngine;

namespace Software10101.Utils {
	public class DestroyOnLoad : MonoBehaviour {
		private void Start () {
			Destroy(gameObject);
		}
	}
}
