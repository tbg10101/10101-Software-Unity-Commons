namespace UnityEngine {
	public static class Vector3Extensions {
		public static Vector3 limitMagnitude (this Vector3 v, float maxMagnitude) {
			if (v.sqrMagnitude <= maxMagnitude * maxMagnitude) {
				return v;
			}

			return (v.normalized * maxMagnitude);
		}

		public static Vector3 pow (this Vector3 v, float power) {
			return new Vector3(Mathf.Pow(v.x, power), Mathf.Pow(v.y, power), Mathf.Pow(v.z, power));
		}

		public static Vector3 Pow (this Vector3 v, float power) {
			v.x = Mathf.Pow(v.x, power);
			v.y = Mathf.Pow(v.y, power);
			v.z = Mathf.Pow(v.z, power);

			return v;
		}

		public static Vector3 ToUniformVector3 (this float f) {
			return new Vector3(f, f, f);
		}
	}
}
