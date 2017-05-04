using System;

namespace Software10101.Util {
	/// <summary>
	/// Abstract FrameCache type.
	/// </summary>
	public abstract class FrameCache {
		protected bool _Stale = false;

		/// <summary>
		/// Whether or not the contained value has been updated in the latest frame.
		/// </summary>
		public bool Stale {
			get {
				return _Stale;
			}

			set {
				if (_Stale && value) {
					FrameCacheManager.frameCaches.Remove(this);
				} else if (_Stale && !value) {
					FrameCacheManager.frameCaches.Add(this);
				}

				_Stale = value;
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
		private readonly Func<T> GeneratorFunction;

		private T _Value;

		/// <summary>
		/// Gets the contained value. (or generates the value if stale)
		/// </summary>
		public T Value {
			get {
				if (Stale) {
					Stale = false;

					_Value = GeneratorFunction();
				}

				return _Value;
			}
		}

		/// <summary>
		/// Creates a new <see cref="FrameCache{T}"> instance.
		/// </summary>
		/// <param name="generatorFunction">The function used to generate the value.</param>
		public FrameCache (Func<T> generatorFunction) {
			GeneratorFunction = generatorFunction;

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

			return _Stale.Equals(other._Stale)
				&& GeneratorFunction.Equals(other.GeneratorFunction)
				&& _Value.Equals(other._Value);
		}

		public override int GetHashCode () {
			return 17 * GeneratorFunction.GetHashCode();
		}
	}
}
