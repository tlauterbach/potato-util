using System;
using System.Collections.Generic;

namespace PotatoUtil {

	public class Database<TKey> where TKey : IEquatable<FNVHash> {

		#region Classes

		private interface IDb {
			string Name { get; }
			int Count { get; }
			bool Contains(TKey id);
			object Get(TKey id);
			void Add(TKey id, object item);
			IEnumerable<TKey> GetAllIds();
		}

		private class HashComparer : EqualityComparer<TKey> {
			public override bool Equals(TKey x, TKey y) {
				return x.Equals(y);
			}
			public override int GetHashCode(TKey obj) {
				return obj.GetHashCode();
			}
		}

		private class Db<TValue> : IDb {

			public string Name { get; }
			public int Count { get { return m_items.Count; } }

			Dictionary<TKey, TValue> m_items = new Dictionary<TKey,TValue>();

			public Db(FNVString name) {
				Name = name.ToString();
				m_items = new Dictionary<TKey, TValue>(new HashComparer());
			}

			public bool Contains(TKey hash) {
				return m_items.ContainsKey(hash);
			}
			public object Get(TKey id) {
				if (m_items.TryGetValue(id, out TValue value)) {
					return value;
				} else {
					throw new KeyNotFoundException(string.Format(
						"No item with id `{0}' exists in Database `{1}'.",
						id, Name
					));
				}
			}
			public void Add(TKey id, object item) {
				if (item is TValue obj) {
					if (!m_items.ContainsKey(id)) {
						m_items.Add(id, obj);
					} else {
						throw new Exception(string.Format(
							"Database `{0}' already contains item with id `{1}'",
							Name, id
						));
					}
				} else {
					throw new InvalidOperationException(string.Format(
						"Database `{0}' cannot convert id `{1}' from `{2}' to `{3}'",
						Name, id, item.GetType().Name, typeof(TValue).Name
					));
				}
			}
			public IEnumerable<TKey> GetAllIds() {
				foreach (TKey key in m_items.Keys) {
					yield return key;
				}
			}

		}

		#endregion

		private Dictionary<FNVString, IDb> m_database = new Dictionary<FNVString, IDb>();

		public void Add<TValue>(FNVString db, IEnumerable<TValue> dataset, Func<TValue,TKey> hashGetter) {
			IDb database;
			if (m_database.ContainsKey(db)) {
				database = m_database[db];
			} else {
				database = new Db<TValue>(db);
				m_database.Add(db, database);
			}
			foreach (TValue item in dataset) {
				TKey hash = hashGetter(item);
				if (database.Contains(hash)) {
					throw new Exception(string.Format(
						"Database `{0}' already contains key `{1}'",
						db, hash.ToString()
					));
				}
				database.Add(hashGetter(item), item);
			}
		}
		public void Remove(FNVString db) {
			m_database.Remove(db);
		}

		public void Clear() {
			m_database.Clear();
		}

		public TValue Get<TValue>(FNVString db, TKey id) {
			if (m_database.ContainsKey(db)) {
				return (TValue)m_database[db].Get(id);
			} else {
				throw new KeyNotFoundException(string.Format(
					"No Database with name `{0}' exists", db
				));
			}
		}
		public bool Contains(FNVString db) {
			return m_database.ContainsKey(db);
		}
		public bool Contains(FNVString db, TKey id) {
			if (m_database.ContainsKey(db)) {
				return m_database[db].Contains(id);
			} else {
				return false;
			}
		}
		public IEnumerable<TKey> GetAllIds(FNVString db) {
			if (m_database.ContainsKey(db)) {
				return m_database[db].GetAllIds();
			} else {
				throw new KeyNotFoundException(string.Format(
					"No Database with name `{0}' exists", db
				));
			}
		}

	}

}