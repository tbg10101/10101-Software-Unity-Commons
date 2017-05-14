using System;

namespace Software10101.Utils {
	/// <summary>
	/// Abstract FrameCache type.
	/// </summary>
	public abstract class FrameCache {
		protected bool _stale = false;

		/// <summary>
		/// Whether or not the contained value has been updated in the latest frame.
		/// </summary>
		public bool Stale {
			get {
				return _stale;
			}

			set {
				if (_stale && value) {
					FrameCacheManager.FrameCaches.Remove(this);
				} else if (_stale && !value) {
					FrameCacheManager.FrameCaches.Add(this);
				}

				_stale = value;
			}
		}
	}

	/// <summary>
	/// This can be used to store a value that you only want to calculate once per frame, must be accessable across multiple objects, but you don't want to worry about script execution order. 
	/// 
	/// The first retrival of Value each frame will calculate the value. Subsaquent retrivals of Value in the same frame will recall the same value, without recalulation.
	/// </summary>
	/// <typeparam name="T">The type of the contained generated object.</typeparam>
	public sealed class FrameCache<T> : FrameCache {
		private readonly Func<T> _generatorFunction;

		private T _value;

		/// <summary>
		/// Gets the contained value. (or generates the value if stale)
		/// </summary>
		public T Value {
			get {
				if (!Stale) {
					return _value;
				}

				Stale = false;

				_value = _generatorFunction();

				return _value;
			}
		}

		/// <summary>
		/// Creates a new <see cref="FrameCache{T}"/> instance.
		/// </summary>
		/// <param name="generatorFunction">The function used to generate the value.</param>
		public FrameCache (Func<T> generatorFunction) {
			_generatorFunction = generatorFunction;

			FrameCacheManager.FrameCaches.Add(this);
		}

		public override bool Equals (object o) {
			if (o == null) {
				return false;
			}

			FrameCache<T> other = o as FrameCache<T>;

			if (other == null) {
				return false;
			}

			return _stale.Equals(other._stale)
				&& _generatorFunction.Equals(other._generatorFunction)
				&& _value.Equals(other._value);
		}

		public override int GetHashCode () {
			return 17 * _generatorFunction.GetHashCode();
		}
	}
}
