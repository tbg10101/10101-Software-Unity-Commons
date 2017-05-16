using System;

namespace Software10101.Utils {
	/// <summary>
	/// Abstract FrameCache type.
	/// </summary>
	public abstract class FrameCache {
		protected bool _stale = true;

		/// <summary>
		/// Whether or not the contained value has been updated in the latest frame.
		/// </summary>
		public bool Stale {
			get {
				return _stale;
			}

			set {
				if (!_stale && value) {
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
	/// The first retrival of Value each frame will calculate the value. Subsequent retrivals of Value in the same frame will recall the same value, without recalulation.
	/// </summary>
	/// <typeparam name="T">The type of the contained generated object.</typeparam>
	public sealed class FrameCache<T> : FrameCache {
		private readonly Func<T> _generatorFunction;

		private T _value;

		/// <summary>
		/// Gets the contained value. (generates the value if stale)
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
		/// Creates a new <see cref="FrameCache{T}"/> instance. The <see cref="FrameCache{T}"/> starts stale.
		/// </summary>
		/// <param name="generatorFunction">The function used to generate the value.</param>
		public FrameCache (Func<T> generatorFunction) {
			_generatorFunction = generatorFunction;

			FrameCacheManager.FrameCaches.Add(this);
		}

		/// <summary>
		/// Creates a new <see cref="FrameCache{T}"/> instance with the given initial value. The <see cref="FrameCache{T}"/> is initially NOT stale.
		/// This is especially useful when caching a collection. The generator function can modify the contained collection then return the same
		/// collection, preventing the need to create a new collection every time the gernator function is called.
		/// </summary>
		/// <param name="generatorFunction">The function used to generate the value.</param>
		/// <param name="initialValue">The value to which the <see cref="FrameCache{T}"/>'s value will be set for the first frame.</param>
		public FrameCache (Func<T> generatorFunction, T initialValue) {
			_generatorFunction = generatorFunction;

			FrameCacheManager.FrameCaches.Add(this);

			_stale = false;

			_value = initialValue;
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

		/// <summary>
		/// Implicit value getter.
		/// </summary>
		/// <param name="fc">The <see cref="FrameCache{T}"/> from which the value will be retrieved.</param>
		/// <returns>Gets the contained value. (generates the value if stale)</returns>
		public static implicit operator T (FrameCache<T> fc) {
			return fc.Value;
		}
	}
}
