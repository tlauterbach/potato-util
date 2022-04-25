using System;
using System.Collections.Generic;

namespace PotatoUtil {

	public interface IPool<T> where T : class {
		int Size { get; }
		int Available { get; }
		T Get();
		void Release(T value);
	}

	/// <summary>
	/// Simple, dumb generic pool implementation that
	/// has a fixed size of the given class type.
	/// </summary>
	public class Pool<T> : IPool<T> where T : class {

		private List<T> m_available;
		private List<T> m_taken;

		public int Size { get { return m_available.Count + m_taken.Count; } }

		public int Available { get { return m_available.Count; } }

		public event Action<T> OnTaken;
		public event Action<T> OnReleased;

		public Pool(byte size, Func<T> constructor) {
			m_available = new List<T>();
			while (size > 0) {
				m_available.Add(constructor());
				size--;
			}
			m_taken = new List<T>();
		}

		public T Get() {
			if (m_available.Count > 0) {
				T item = m_available[0];
				m_taken.Add(item);
				m_available.Remove(item);
				OnTaken?.Invoke(item);
				return item;
			} else {
				throw new InvalidOperationException("All items in the pool " +
					"are currently in use. Did you forget to release them?"
				);
			}
		}
		public void Release(T value) {
			if (m_taken.Contains(value)) {
				m_taken.Remove(value);
				m_available.Add(value);
				OnReleased?.Invoke(value);
			} else {
				throw new InvalidOperationException("Trying to release item " +
					"to the pool that did not originate from this pool!"
				);
			}
		}

	}

}