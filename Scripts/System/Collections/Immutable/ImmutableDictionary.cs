using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Immutable {
	public sealed class ImmutableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary {
		public static readonly ImmutableDictionary<TKey, TValue> Empty = new ImmutableDictionary<TKey, TValue>();
		
		private readonly Dictionary<TKey, TValue> _dictionary;

		public ImmutableDictionary (params KeyValuePair<TKey, TValue>[] entries) {
			_dictionary = new Dictionary<TKey, TValue>(entries.Length);
			
			entries.ForEach(e => _dictionary[e.Key] = e.Value);
		}
		
		public ImmutableDictionary (IDictionary<TKey, TValue> original) {
			_dictionary = new Dictionary<TKey, TValue>(original.Count);
			
			original.ForEach(e => _dictionary[e.Key] = e.Value);
		}
		
		void IDictionary.Clear () { }

		IDictionaryEnumerator IDictionary.GetEnumerator () {
			return _dictionary.GetEnumerator();
		}

		public void Remove (object key) { }

		object IDictionary.this [object key] {
			get {
				try {
					return _dictionary[(TKey) key];
				} catch (Exception) {
					throw new KeyNotFoundException();
				}
			}
			
			set { }
		}

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator () {
			return _dictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator () {
			return _dictionary.GetEnumerator();
		}

		public void Add (KeyValuePair<TKey, TValue> item) { }

		public bool Contains (object key) {
			try {
				return _dictionary.ContainsKey((TKey)key);
			} catch (Exception) {
				return false;
			}
		}

		public void Add (object key, object value) { }

		void ICollection<KeyValuePair<TKey, TValue>>.Clear () { }

		public bool Contains (KeyValuePair<TKey, TValue> item) {
			return _dictionary.ContainsKey(item.Key) && _dictionary[item.Key].Equals(item.Value);
		}

		public void CopyTo (KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
			Array.Copy(_dictionary.ToArray(), arrayIndex, array, 0, _dictionary.Count);
		}

		public bool Remove (KeyValuePair<TKey, TValue> item) {
			return false;
		}

		public void CopyTo (Array array, int index) {
			Array.Copy(_dictionary.ToArray(), index, array, 0, _dictionary.Count);
		}

		int ICollection.Count {
			get {
				return _dictionary.Count;
			}
		}

		public object SyncRoot {
			get {
				return ((IDictionary)_dictionary).SyncRoot;
			}
		}

		public bool IsSynchronized {
			get {
				return true;
			}
		}

		int ICollection<KeyValuePair<TKey, TValue>>.Count {
			get {
				return _dictionary.Count;
			}
		}

		ICollection IDictionary.Values {
			get {
				return _dictionary.Values;
			}
		}

		bool IDictionary.IsReadOnly {
			get {
				return true;
			}
		}

		public bool IsFixedSize {
			get {
				return true;
			}
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly {
			get {
				return true;
			}
		}

		public bool ContainsKey (TKey key) {
			return _dictionary.ContainsKey(key);
		}

		public void Add (TKey key, TValue value) { }

		public bool Remove (TKey key) {
			return false;
		}

		public bool TryGetValue (TKey key, out TValue value) {
			return _dictionary.TryGetValue(key, out value);
		}

		TValue IDictionary<TKey, TValue>.this [TKey key] {
			get {
				return _dictionary[key];
			}
			
			set { }
		}

		ICollection<TKey> IDictionary<TKey, TValue>.Keys {
			get {
				return _dictionary.Keys;
			}
		}

		ICollection IDictionary.Keys {
			get {
				return _dictionary.Keys;
			}
		}

		ICollection<TValue> IDictionary<TKey, TValue>.Values {
			get {
				return _dictionary.Values;
			}
		}
	}
	
	
}
