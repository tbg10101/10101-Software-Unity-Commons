using System.Linq;
using System.Threading;

namespace System.Collections.Generic {
    public sealed class HashQueue<T> : ICollection, IReadOnlyCollection<T> {
        private readonly HashSet<T> _hashSet;
        private readonly Queue<T> _queue;

        private object _syncRoot;

        public int Count => _queue.Count;

        public HashQueue() {
            _hashSet = new HashSet<T>();
            _queue = new Queue<T>();
        }

        public HashQueue(int capacity) {
            _hashSet = new HashSet<T>();
            _queue = new Queue<T>(capacity);
        }

        public HashQueue(IEnumerable<T> source) {
            _hashSet = new HashSet<T>(source);
            _queue = new Queue<T>(_hashSet);
        }

        public void Clear() {
            _hashSet.Clear();
            _queue.Clear();
        }

        public bool Contains(T element) {
            return _hashSet.Contains(element);
        }

        public T Dequeue() {
            T element = _queue.Dequeue();
            _hashSet.Remove(element);
            return element;
        }

        public void Enqueue(T element) {
            if (_hashSet.Add(element)) {
                _queue.Enqueue(element);
            }
        }

        public T Peek() {
            return _queue.Peek();
        }

        public void TrimExcess() {
            _queue.TrimExcess();
            _hashSet.TrimExcess();
        }

        private bool Equals(HashQueue<T> other) {
            return _hashSet.Equals(other._hashSet) && _queue.Equals(other._queue);
        }

        public override bool Equals(object obj) {
            return ReferenceEquals(this, obj) || obj is HashQueue<T> other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return (_hashSet.GetHashCode() * 397) ^ _queue.GetHashCode();
            }
        }

        public Queue<T>.Enumerator GetEnumerator() {
            return _queue.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return _queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _queue.GetEnumerator();
        }

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot {
            get {
                if (_syncRoot == null)
                    Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                return _syncRoot;
            }
        }

        void ICollection.CopyTo(Array array, int index) {
            this.ToArray().CopyTo(array, index);
        }

        public void CopyTo(T[] array, int index) {
            _queue.CopyTo(array, index);
        }
    }
}
