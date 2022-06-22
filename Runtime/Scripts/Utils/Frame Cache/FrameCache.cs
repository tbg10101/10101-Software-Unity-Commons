using System;
using UnityEngine;

namespace Software10101.Utils {
    /// <summary>
    /// Abstract FrameCache type.
    /// </summary>
    public abstract class FrameCache {
        private int _lastFrameValueGenerated = -1;

        /// <summary>
        /// Whether or not the contained value has been updated in the current frame.
        /// </summary>
        public bool Stale => _lastFrameValueGenerated != Time.frameCount;

        protected void OnValueGenerated() {
            _lastFrameValueGenerated = Time.frameCount;
        }
    }

    /// <summary>
    /// This can be used to store a value that you only want to calculate once per frame, must be accessible across multiple
    /// objects, but you don't want to worry about script execution order.
    ///
    /// The first retrieval of Value each frame will calculate the value. Subsequent retrievals of Value in the same frame will
    /// recall the same value, without recalculation.
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

                _value = _generatorFunction();

                OnValueGenerated();

                return _value;
            }
        }

        /// <summary>
        /// Creates a new <see cref="FrameCache{T}"/> instance. The <see cref="FrameCache{T}"/> starts stale.
        /// </summary>
        /// <param name="generatorFunction">The function used to generate the value.</param>
        public FrameCache(Func<T> generatorFunction) {
            _generatorFunction = generatorFunction;
        }

        /// <summary>
        /// Creates a new <see cref="FrameCache{T}"/> instance with the given initial value. The <see cref="FrameCache{T}"/> is
        /// initially NOT stale. This is especially useful when caching a collection. The generator function can modify the
        /// contained collection then return the same collection, preventing the need to create a new collection every time the
        /// generator function is called.
        /// </summary>
        /// <param name="generatorFunction">The function used to generate the value.</param>
        /// <param name="initialValue">The value to which the <see cref="FrameCache{T}"/>'s value will be set for the first frame.</param>
        public FrameCache(Func<T> generatorFunction, T initialValue) {
            _generatorFunction = generatorFunction;
            _value = initialValue;

            OnValueGenerated();
        }

        public override bool Equals (object o) {
            if (!(o is FrameCache<T> other)) {
                return false;
            }

            return Stale.Equals(other.Stale)
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
