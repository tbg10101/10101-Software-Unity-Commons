using System;

namespace Software10101.Util {
	public abstract class FrameCache {
		protected uint _staleness = 0;

		public uint staleness {
			get {
				return _staleness;
			}

			set {
				_staleness = value;

				if (_staleness >= stalenessLimit) {
					FrameCacheManager.frameCaches.Remove(this);
				}
			}
		}

		public readonly uint stalenessLimit;

		protected FrameCache (uint stalenessLimit) {
			this.stalenessLimit = stalenessLimit;
		}
	}

	public sealed class FrameCache<T> : FrameCache {
		private readonly Func<T> generatorFunction;

		private T _value;

		public T value {
			get {
				if (staleness != 0) {
					staleness = 0;

					_value = generatorFunction();
				}

				return _value;
			}
		}

		public FrameCache (Func<T> generatorFunction, uint stalenessLimit) : base(stalenessLimit) {
			this.generatorFunction = generatorFunction;

			FrameCacheManager.frameCaches.Add(this);
		}

		public override bool Equals (object o) {
			if (o == null) {
				return false;
			}

			FrameCache<T> other = o as FrameCache<T>;

			if (other == null) {
				return false;
			}

			return _staleness.Equals(other._staleness)
				&& stalenessLimit.Equals(other.stalenessLimit)
				&& generatorFunction.Equals(other.generatorFunction)
				&& _value.Equals(other._value);
		}

		public override int GetHashCode () {
			return 17 * stalenessLimit.GetHashCode()
				+ 31 * generatorFunction.GetHashCode();
		}
	}
}
