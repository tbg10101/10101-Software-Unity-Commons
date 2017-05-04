// TODO remove after getting .NET 4.0 or later
namespace System {
	public static class Tuple {
		/// <summary>
		/// Creates a new 1-tuple, or singleton.
		/// </summary>
		/// <typeparam name="T1">The type of the only component of the tuple.</typeparam>
		/// <param name="Item1">The value of the only component of the tuple.</param>
		/// <returns></returns>
		public static Tuple<T1> Create<T1> (T1 Item1) {
			return new Tuple<T1>(Item1);
		}

		/// <summary>
		/// Creates a new 2-tuple, or pair.
		/// </summary>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <param name="Item1">The value of the first component of the tuple.</param>
		/// <param name="Item2">The value of the second component of the tuple.</param>
		/// <returns></returns>
		public static Tuple<T1, T2> Create<T1, T2> (T1 Item1, T2 Item2) {
			return new Tuple<T1, T2>(Item1, Item2);
		}
	}

	/// <summary>
	/// Represents a 1-tuple, or singleton.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's only component.</typeparam>
	public class Tuple<T1> {
		/// <summary>
		/// The value of the current <see cref="Tuple{T1}"/> object's single component.
		/// </summary>
		public readonly T1 Item1;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tuple{T1}"/> class.
		/// </summary>
		/// <param name="Item1">The value of the tuple's only component.</param>
		public Tuple (T1 Item1) {
			this.Item1 = Item1;
		}
	}

	/// <summary>
	/// Represents a 2-tuple, or pair.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	public class Tuple<T1, T2> {
		/// <summary>
		/// The value of the current <see cref="Tuple{T1, T2}"/> object's first component.
		/// </summary>
		public readonly T1 Item1;

		/// <summary>
		/// The value of the current <see cref="Tuple{T1, T2}"/> object's second component.
		/// </summary>
		public readonly T2 Item2;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tuple{T1, T2}"/> class.
		/// </summary>
		/// <param name="Item1">The value of the tuple's first component.</param>
		/// <param name="Item2">The value of the tuple's second component.</param>
		public Tuple (T1 Item1, T2 Item2) {
			this.Item1 = Item1;
			this.Item2 = Item2;
		}
	}
}
