using System;
using System.Collections.Generic;

namespace PotatoUtil {

	public class ReadOnlyDatabase<TKey, TVal> where TKey : IEquatable<TKey> where TVal : class {

		public int Count {
			get { return m_database.Count; }
		}
		public IEnumerable<TKey> Keys {
			get {
				foreach (TKey key in m_database.Keys) {
					yield return key;
				}
			}
		}
		public IEnumerable<TVal> Values {
			get {
				foreach (TVal value in m_database.Values) {
					yield return value;
				}
			}
		}
		public TVal this[TKey key] {
			get { return Get(key); }
		}

		private Dictionary<TKey, TVal> m_database;

		public ReadOnlyDatabase(Database<TKey,TVal> original) {
			if (original == null) {
				throw new ArgumentNullException();
			}
			m_database = new Dictionary<TKey, TVal>();
			foreach (TKey key in original.Keys) {
				m_database.Add(key, original[key]);
			}
		}

		public TVal Get(TKey key) {
			if (m_database.ContainsKey(key)) {
				return m_database[key];
			} else {
				throw new KeyNotFoundException(string.Format(
					"No value with key `{0}' exists in the " +
					"Database", key.ToString()
				));
			}
		}
		public TVal FindFirst(Predicate<TVal> query) {
			foreach (TVal value in m_database.Values) {
				if (query(value)) {
					return value;
				}
			}
			return null;
		}
		public IEnumerable<TVal> FindAll(Predicate<TVal> query) {
			foreach (TVal value in m_database.Values) {
				if (query(value)) {
					yield return value;
				}
			}
		}

		public bool ContainsKey(TKey key) {
			return m_database.ContainsKey(key);
		}

	}

}