namespace UnityEngine {
	public static class Vector3Extensions {
		public static Vector3 LimitMagnitude (this Vector3 v, float maxMagnitude) {
			if (v.sqrMagnitude <= maxMagnitude * maxMagnitude) {
				return v;
			}

			return (v.normalized * maxMagnitude);
		}
	}
}
