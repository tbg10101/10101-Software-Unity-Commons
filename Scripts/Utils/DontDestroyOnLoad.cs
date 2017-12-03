using UnityEngine;

namespace Software10101.Utils {
	public class DontDestroyOnLoad : MonoBehaviour {
		private void Start () {
			DontDestroyOnLoad(gameObject);
		}
	}
}
