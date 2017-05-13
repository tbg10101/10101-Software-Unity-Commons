using System.Linq;

namespace System.Collections.Generic {
	/// <summary>
	/// Based on Bradley Odell's version here: http://stackoverflow.com/a/31419199/2669980
	/// </summary>
	/// <typeparam name="T">The type contained by the set.</typeparam>
	public sealed class LinkedHashSet<T> : ISet<T> {
		private readonly IDictionary<T, LinkedListNode<T>> _dict;
		private readonly LinkedList<T> _list;

		public LinkedHashSet (int initialCapacity) {
			_dict = new Dictionary<T, LinkedListNode<T>>(initialCapacity);
			_list = new LinkedList<T>();
		}

		public LinkedHashSet () {
			_dict = new Dictionary<T, LinkedListNode<T>>();
			_list = new LinkedList<T>();
		}

		public LinkedHashSet (IEnumerable<T> e) : this() {
			AddEnumerable(e);
		}

		public LinkedHashSet (int initialCapacity, IEnumerable<T> e) : this(initialCapacity) {
			AddEnumerable(e);
		}

		private void AddEnumerable (IEnumerable<T> e) {
			foreach (T t in e) {
				Add(t);
			}
		}

		//
		// ISet implementation
		//

		public bool Add (T item) {
			if (_dict.ContainsKey(item)) {
				return false;
			}

			LinkedListNode<T> node = _list.AddLast(item);
			_dict[item] = node;

			return true;
		}

		public void ExceptWith (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			foreach (T t in other) {
				Remove(t);
			}
		}

		public void IntersectWith (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			T[] ts = new T[Count];

			CopyTo(ts, 0);

			foreach (T t in ts) {
				if (!other.Contains(t)) {
					Remove(t);
				}
			}
		}

		public bool IsProperSubsetOf (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			int contains = 0;
			int noContains = 0;

			foreach (T t in other) {
				if (Contains(t)) {
					contains++;
				} else {
					noContains++;
				}
			}

			return contains == Count && noContains > 0;
		}

		public bool IsProperSupersetOf (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			int otherCount = other.Count();

			if (Count <= otherCount) {
				return false;
			}

			int contains = 0;
			int noContains = 0;

			foreach (T t in this) {
				if (other.Contains(t)) {
					contains++;
				} else {
					noContains++;
				}
			}

			return contains == otherCount && noContains > 0;
		}

		public bool IsSubsetOf (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			foreach (T t in this) {
				if (!other.Contains(t)) {
					return false;
				}
			}

			return true;
		}

		public bool IsSupersetOf (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			foreach (T t in other) {
				if (!Contains(t)) {
					return false;
				}
			}

			return true;
		}

		public bool Overlaps (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			foreach (T t in other) {
				if (Contains(t)) {
					return true;
				}
			}

			return false;
		}

		public bool SetEquals (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			int otherCount = other.Count();

			return Count == otherCount && IsSupersetOf(other);
		}

		public void SymmetricExceptWith (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			T[] ts = new T[Count];

			CopyTo(ts, 0);

			HashSet<T> otherList = new HashSet<T>(other);

			foreach (T t in ts) {
				if (!otherList.Contains(t)) {
					continue;
				}

				Remove(t);
				otherList.Remove(t);
			}

			foreach (T t in otherList) {
				Add(t);
			}
		}

		public void UnionWith (IEnumerable<T> other) {
			if (other == null) {
				throw new ArgumentNullException("other", "other cannot be null");
			}

			foreach (T t in other) {
				Add(t);
			}
		}

		//
		// ICollection<T> implementation
		//

		public int Count {
			get {
				return _dict.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return _dict.IsReadOnly;
			}
		}

		void ICollection<T>.Add (T item) {
			Add(item);
		}

		public void Clear () {
			_dict.Clear();
			_list.Clear();
		}

		public bool Contains (T item) {
			return _dict.ContainsKey(item);
		}

		public void CopyTo (T[] array, int arrayIndex) {
			_list.CopyTo(array, arrayIndex);
		}

		public bool Remove (T item) {
			LinkedListNode<T> node;

			if (!_dict.TryGetValue(item, out node)) {
				return false;
			}

			_dict.Remove(item);
			_list.Remove(node);

			return true;
		}

		//
		// IEnumerable<T> implementation
		//

		public IEnumerator<T> GetEnumerator () {
			return _list.GetEnumerator();
		}

		//
		// IEnumerable implementation
		//

		IEnumerator IEnumerable.GetEnumerator () {
			return _list.GetEnumerator();
		}
	}
}
