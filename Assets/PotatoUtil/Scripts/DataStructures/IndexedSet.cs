using System;
using System.Collections;
using System.Collections.Generic;

namespace PotatoUtil {

	/// <summary>
	/// A simple, wrapped implementation of the IReadOnlyList<typeparamref name="T"/> 
	/// interface that has more practical construction options
	/// </summary>
	public class IndexedSet<T> : IReadOnlyList<T> where T : IEquatable<T> {

		public T this[int index] {
			get {
				if (index < 0 || index >= Count) {
					throw new IndexOutOfRangeException();
				}
				return m_set[index];
			}
		}
		public T this[uint index] {
			get {
				if (index < 0 || index >= Count) {
					throw new IndexOutOfRangeException();
				}
				return m_set[index];
			}
		}

		public int Count { get { return m_set.Length; } }

		private T[] m_set;

		public IndexedSet(IList<T> collection) : this((IEnumerable<T>)collection) {
		}
		public IndexedSet(params T[] collection) : this((IEnumerable<T>)collection) {
		}
		public IndexedSet(IEnumerable<T> collection) {
			int count = 0;
			foreach (T t in collection) {
				count++;
			}
			m_set = new T[count];
			foreach (T t in collection) {
				m_set[m_set.Length - (count--)] = t;
			}
		}

		public int IndexOf(T item) {
			return Array.IndexOf(m_set, item);
		}
		public bool Contains(T item) {
			for (int index = 0; index < m_set.Length; index++) {
				if (m_set[index].Equals(item)) {
					return true;
				}
			}
			return false;
		}

		public IEnumerator<T> GetEnumerator() {
			return ((IEnumerable<T>)m_set).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return m_set.GetEnumerator();
		}
	}

}
