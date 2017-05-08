using System.Collections.Generic;
using UnityEngine;

namespace Software10101.Utils {
	/// <summary>
	/// Tracks the stale-ness of individual <see cref="FrameCache"> objects.
	/// 
	/// Ensure that this runs before all scripts which use <see cref="FrameCache"> objects in the Unity execution order setting.
	/// </summary>
	public class FrameCacheManager : MonoBehaviour {
		/// <summary>
		/// The set of <see cref="FrameCache"> objects that are being tracked for stale-ness. <see cref="FrameCache{T}"> objects that are stale for 2 frames in-a-row are removed automatically.
		/// </summary>
		public static HashSet<FrameCache> frameCaches = new HashSet<FrameCache>();

		/// <summary>
		/// Whether or not the object should ersist between scene changes. Only one <see cref="FrameCacheManager"> will exist at any time, regarless of this selection.
		/// </summary>
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
			frameCaches.ForEach(fc => fc.Stale = true);
		}
	}
}
