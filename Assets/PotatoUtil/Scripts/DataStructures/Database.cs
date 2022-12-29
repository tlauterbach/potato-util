using System;
using System.Collections.Generic;

namespace PotatoUtil {

	public class Database<TKey, TVal> where TKey : IEquatable<TKey> where TVal : class {

		private class HashComparer : EqualityComparer<TKey> {
			public override bool Equals(TKey x, TKey y) {
				return x.Equals(y);
			}
			public override int GetHashCode(TKey obj) {
				return obj.GetHashCode();
			}
		}

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
		private readonly Func<TVal, TKey> m_keyGetter;

		#region Constructors

		public Database(Func<TVal, TKey> hashGetter) {
			m_database = new Dictionary<TKey, TVal>(new HashComparer());
			m_keyGetter = hashGetter;
		}
		public Database(Func<TVal, TKey> hashGetter, IEnumerable<TVal> values) : this(hashGetter) {
			Add(values);
		}
		public Database(Func<TVal, TKey> hashGetter, params TVal[] values) : this(hashGetter, (IEnumerable<TVal>)values) {

		}

		#endregion

		public void Add(TVal value) {
			TKey key = m_keyGetter(value);
			if (m_database.ContainsKey(key)) {
				throw new InvalidOperationException(string.Format(
					"Value with Key `{0}' already exists " +
					"in the Database", key.ToString()
				));
			}
			m_database.Add(key, value);
		}
		public void Add(params TVal[] values) {
			Add((IEnumerable<TVal>)values);
		}
		public void Add(IEnumerable<TVal> values) {
			foreach (TVal value in values) {
				Add(value);
			}
		}
		public void Remove(TKey key) {
			m_database.Remove(key);
		}
		public void Remove(IEnumerable<TKey> keys) {
			foreach (TKey key in keys) {
				Remove(key);
			}
		}
		public void Clear() {
			m_database.Clear();
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

		public bool TryGet(TKey key, out TVal value) {
			return m_database.TryGetValue(key, out value);
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

		public ReadOnlyDatabase<TKey,TVal> AsReadOnly() {
			return new ReadOnlyDatabase<TKey,TVal>(this);
		}

	}

}