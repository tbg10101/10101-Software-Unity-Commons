namespace System.Collections.Generic {
	/// <summary>
	/// This class consists exclusively of static methods that operate on or return collections.
	/// </summary>
	public static class Collections {
		/// <summary>
		/// Returns a list containing only the specified object.
		/// </summary>
		/// <typeparam name="T">The type of the objects in the list.</typeparam>
		/// <param name="o">The sole object to be stored in the returned list.</param>
		/// <returns>A list containing only the specified object.</returns>
		public static List<T> SingletonList<T>(T o) {
			return new List<T> { o };
		}

		public static List<T> ToSingletonList<T>(this T obj) {
			return new List<T>(1) {obj};
		}

		/// <summary>
		/// Returns an empty list.
		/// </summary>
		/// <typeparam name="T">The type of the objects in the list.</typeparam>
		/// <returns>An empty list.</returns>
		public static List<T> EmptyList<T>() {
			return new List<T>();
		}

		/// <summary>
		/// Performs each <see cref="Action"/> delegate in the <see cref="IEnumerable"/>.
		/// </summary>
		/// <param name="collection">The <see cref="IEnumerable"/> to be iterated over.</param>
		public static void InvokeEach(this IEnumerable<Action> collection) {
			foreach (Action action in collection) {
				action.Invoke();
			}
		}

		/// <summary>
		/// Performs each <see cref="Action{T1}"/> delegate in the <see cref="IEnumerable"/>.
		/// </summary>
		/// <typeparam name="T">The type of the objects in the <see cref="IEnumerable"/>.</typeparam>
		/// <param name="collection">The <see cref="IEnumerable"/> to be iterated over.</param>
		/// <param name="param">The parameter to provide to each <see cref="Action{T1}"/> delegate.</param>
		public static void InvokeEach<T>(this IEnumerable<Action<T>> collection, T param) {
			foreach (Action<T> action in collection) {
				action.Invoke(param);
			}
		}

		/// <summary>
		/// Performs the specified <see cref="Action{T1}"/> on each element of the <see cref="IEnumerable{T}"/>.
		/// </summary>
		/// <typeparam name="T">The type of the objects in the <see cref="IEnumerable"/>.</typeparam>
		/// <param name="sequence">The <see cref="IEnumerable"/> to be iterated over.</param>
		/// <param name="action">The <see cref="Action{T1}"/> delegate to perform on each element of the <see cref="IEnumerable"/>.</param>
		public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action) {
			if (sequence == null) {
				return;
			}

			foreach (T item in sequence) {
				action.Invoke(item);
			}
		}

		/// <summary>
		/// Performs the specified <see cref="Action{T1, T2}"/> on each <see cref="KeyValuePair{TKey, TValue}"/> of the <see cref="IEnumerable"/>.
		/// </summary>
		/// <typeparam name="TKey">The type of the keys in the <see cref="IEnumerable"/>.</typeparam>
		/// <typeparam name="TValue">The type of the values in the <see cref="IEnumerable"/>.</typeparam>
		/// <param name="sequence">The <see cref="IEnumerable"/> to be iterated over.</param>
		/// <param name="action">The <see cref="Action{T1, T2}"/> delegate to perform on each element of the <see cref="IEnumerable"/>.</param>
		public static void ForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> sequence, Action<TKey, TValue> action) {
			foreach (KeyValuePair<TKey, TValue> item in sequence) {
				action.Invoke(item.Key, item.Value);
			}
		}

		/// <summary>
		/// Performs the specified <see cref="Func{T, TResult}"/> on each element of the input <see cref="IEnumerable{T}"/>, mapping each element from
		/// T to R. The result is an <see cref="IEnumerable{R}"/> in the same order as the input./>
		/// </summary>
		/// <typeparam name="T">The input type of the objects in the input <see cref="IEnumerable"/>.</typeparam>
		/// <typeparam name="TResult">The output type of the objects in the returned <see cref="IEnumerable"/>.</typeparam>
		/// <param name="sequence">The <see cref="IEnumerable"/> to be iterated over.</param>
		/// <param name="action">The <see cref="Func{T, TResult}"/> delegate to perform on each element of the <see cref="IEnumerable"/> which maps
		/// from T to R.</param>
		public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> sequence, Func<T, TResult> action) {
			IList<TResult> output = new List<TResult>();

			foreach (T item in sequence) {
				output.Add(action(item));
			}

			return output;
        }

        /// <summary>
        /// Performs the specified <see cref="Func{T, TResult}"/> on each element of the input <see cref="IList{T}"/>, mapping each element from
        /// T to R. The result is an <see cref="IList{R}"/> in the same order as the input./>
        /// </summary>
        /// <typeparam name="T">The input type of the objects in the input <see cref="IList"/>.</typeparam>
        /// <typeparam name="TResult">The output type of the objects in the returned <see cref="IList"/>.</typeparam>
        /// <param name="sequence">The <see cref="IList"/> to be iterated over.</param>
        /// <param name="action">The <see cref="Func{T, TResult}"/> delegate to perform on each element of the <see cref="IList"/> which maps
        /// from T to R.</param>
        public static IList<TResult> Map<T, TResult>(this IList<T> sequence, Func<T, TResult> action) {
            IList<TResult> output = new List<TResult>();

            foreach (T item in sequence) {
                output.Add(action(item));
            }

            return output;
        }

        public static T[] ToArray<T>(params T[] array) {
			return array;
		}

		public static bool Equals(IEnumerable a, IEnumerable b) {
			IEnumerator aEnumerator = a.GetEnumerator();
			IEnumerator bEnumerator = b.GetEnumerator();

			while (true) {
				bool aHasNext = aEnumerator.MoveNext();
				bool bHasNext = bEnumerator.MoveNext();

				if (aHasNext && bHasNext) {
					if (!Equals(aEnumerator.Current, bEnumerator.Current)) {
						return false; // the two current elements are not equal
					}
				} else if (!aHasNext && !bHasNext) {
					return true; // we got through both!
				} else {
					return false; // a and b have different lengths
				}
			}
		}

		public static void AddAll<K, V>(this IDictionary<K, V> me, IDictionary<K, V> other) {
			other.ForEach(
				entry => {
					me[entry.Key] = entry.Value;
				});
		}

        public static void AddAll<T>(this IList<T> me, IEnumerable<T> other) {
            other.ForEach(me.Add);
        }

        public static IDictionary<K, V> Clone<K, V>(this IDictionary<K, V> me) {
            IDictionary<K, V> result = new Dictionary<K, V>();

            me.ForEach(
                entry => {
                    result.Add(entry.Key, entry.Value);
                });

            return result;
        }

        public static T LastElement<T>(this IList<T> me) {
            return me[me.Count - 1];
        }

        // ReSharper disable once UseDeconstructionOnParameter // deconstructing here is recursion and will cause a stack overflow
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key, out TValue value) {
	        key = kvp.Key;
	        value = kvp.Value;
        }

        public static T MinElement<T>(this IEnumerable<T> source, Func<T, IComparable> selector) {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null) {
                throw new ArgumentNullException(nameof(selector));
            }

            IComparable minimum = null;
            T value = default;

            foreach (T v in source) {
                IComparable current = selector.Invoke(v);

                if (current == null) {
                    throw new NullReferenceException();
                }

                if (minimum == null || current.CompareTo(minimum) < 0) {
                    minimum = current;
                    value = v;
                }
            }

            return value;
        }

        public static T1 MinElement<T1, T2>(this IEnumerable<T1> source, Func<T1, T2> selector) where T2 : IComparable<T2> {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null) {
                throw new ArgumentNullException(nameof(selector));
            }

            bool firstIteration = true;

            T2 minimum = default;
            T1 value = default;

            foreach (T1 v in source) {
                T2 current = selector.Invoke(v);

                if (current == null) {
                    throw new NullReferenceException();
                }

                if (firstIteration || current.CompareTo(minimum) < 0) {
                    minimum = current;
                    value = v;
                    firstIteration = false;
                }
            }

            return value;
        }

        public static T MaxElement<T>(this IEnumerable<T> source, Func<T, IComparable> selector) {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null) {
                throw new ArgumentNullException(nameof(selector));
            }

            IComparable maximum = null;
            T value = default;

            foreach (T v in source) {
                IComparable current = selector.Invoke(v);

                if (current == null) {
                    throw new NullReferenceException();
                }

                if (maximum == null || current.CompareTo(maximum) > 0) {
                    maximum = current;
                    value = v;
                }
            }

            return value;
        }

        public static T1 MaxElement<T1, T2>(this IEnumerable<T1> source, Func<T1, T2> selector) where T2 : IComparable<T2> {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null) {
                throw new ArgumentNullException(nameof(selector));
            }

            bool firstIteration = true;

            T2 maximum = default;
            T1 value = default;

            foreach (T1 v in source) {
                T2 current = selector.Invoke(v);

                if (current == null) {
                    throw new NullReferenceException();
                }

                if (firstIteration || current.CompareTo(maximum) > 0) {
                    maximum = current;
                    value = v;
                    firstIteration = false;
                }
            }

            return value;
        }
    }
}
