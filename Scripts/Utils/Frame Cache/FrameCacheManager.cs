using System.Collections.Generic;
using UnityEngine;

namespace Software10101.Util {
	public class FrameCacheManager : MonoBehaviour {
		public static HashSet<FrameCache> frameCaches = new HashSet<FrameCache>();

		public bool dontDestroyOnLoad = false;

		private FrameCacheManager instance = null;

		private void Start () {
			if (instance != null && (instance.dontDestroyOnLoad || instance.dontDestroyOnLoad == dontDestroyOnLoad)) {
				Destroy(this);
				return;
			}

			instance = this;

			if (dontDestroyOnLoad) {
				DontDestroyOnLoad(gameObject);
			}
		} 

		private void Update () {
			frameCaches.ForEach(fc => fc.staleness++);
		}
	}
}
