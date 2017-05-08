namespace UnityEngine {
	public static class Vector3Extensions {
		public static Vector3 limitMagnitude (this Vector3 v, float maxMagnitude) {
			if (v.sqrMagnitude <= maxMagnitude * maxMagnitude) {
				return v;
			}

			return (v.normalized * maxMagnitude);
		}
	}
}
