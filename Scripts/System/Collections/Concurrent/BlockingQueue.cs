using System.Collections.Generic;
using System.Threading;

namespace System.Collections.Concurrent {
	/// <summary>
	/// Based on this StackOverflow answer: https://stackoverflow.com/a/530228/2669980
	/// </summary>
	public class BlockingQueue<T> {
		private readonly Queue<T> _queue = new Queue<T>();
		private readonly int _maxSize;

		public BlockingQueue () {
			_maxSize = int.MaxValue;
		}

		public BlockingQueue (int maxSize) {
			_maxSize = maxSize;
		}

		public void Enqueue(T item) {
			lock (_queue) {
				while (_queue.Count >= _maxSize) {
					Monitor.Wait(_queue);
				}

				_queue.Enqueue(item);
				if (_queue.Count >= 1) {
					// wake up any blocked dequeue
					Monitor.PulseAll(_queue);
				}
			}
		}

		public T Dequeue() {
			lock (_queue) {
				while (_queue.Count == 0) {
					Monitor.Wait(_queue);
				}

				T item = _queue.Dequeue();
				if (_queue.Count == _maxSize - 1) {
					// wake up any blocked enqueue
					Monitor.PulseAll(_queue);
				}
				return item;
			}
		}

		public void ForEach (Action<T> action) {
			lock (_queue) {
				foreach (T item in _queue) {
					action.Invoke(item);
				}
			}
		}

		public void Clear () {
			lock (_queue) {
				_queue.Clear();
			}
		}
	}
}
