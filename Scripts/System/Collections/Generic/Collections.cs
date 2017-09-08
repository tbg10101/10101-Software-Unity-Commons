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
		public static List<T> SingletonList<T> (T o) {
			return new List<T> { o };
		}

		/// <summary>
		/// Returns an empty list.
		/// </summary>
		/// <typeparam name="T">The type of the objects in the list.</typeparam>
		/// <returns>An empty list.</returns>
		public static List<T> EmptyList<T> () {
			return new List<T>();
		}

		/// <summary>
		/// Performs each <see cref="Action"/> delegate in the <see cref="IEnumerable"/>.
		/// </summary>
		/// <param name="collection">The <see cref="IEnumerable"/> to be iterated over.</param>
		public static void InvokeEach (this IEnumerable<Action> collection) {
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
		public static void InvokeEach<T> (this IEnumerable<Action<T>> collection, T param) {
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
		public static void ForEach<T> (this IEnumerable<T> sequence, Action<T> action) {
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
		public static void ForEach<TKey, TValue> (this IEnumerable<KeyValuePair<TKey, TValue>> sequence, Action<TKey, TValue> action) {
			foreach (KeyValuePair<TKey, TValue> item in sequence) {
				action.Invoke(item.Key, item.Value);
			}
		}

		/// <summary>
		/// Performs the specified <see cref="Func{T, TResult}"/> on each element of the input <see cref="IEnumerable{T}"/>, mapping each element from
		/// T to R. The result is an <see cref="IEnumerable{R}"/> in ste same order as the input./>
		/// </summary>
		/// <typeparam name="T">The input type of the objects in the input <see cref="IEnumerable"/>.</typeparam>
		/// <typeparam name="TResult">The output type of the objects in the returned <see cref="IEnumerable"/>.</typeparam>
		/// <param name="sequence">The <see cref="IEnumerable"/> to be iterated over.</param>
		/// <param name="action">The <see cref="Func{T, TResult}"/> delegate to perform on each element of the <see cref="IEnumerable"/> which maps
		/// from T to R.</param>
		public static IEnumerable<TResult> Map<T, TResult> (this IEnumerable<T> sequence, Func<T, TResult> action) {
			List<TResult> output = new List<TResult>();

			foreach (T item in sequence) {
				output.Add(action(item));
			}

			return output;
		}
	}
}
