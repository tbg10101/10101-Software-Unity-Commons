using Software10101.Units;
using UnityEngine;

namespace Software10101.Utils {
	public static class TimeHelper {
		public static readonly FrameCache<Duration> DeltaDuration = new FrameCache<Duration>(() => Duration.SECOND * Time.deltaTime);
	}
}
