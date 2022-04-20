using System;
using System.Collections.Generic;

namespace PotatoUtil {

	public class Database {

		#region Classes

		private interface IDb {
			FNVHash Id { get; }
			string Name { get; }
			int Count { get; }
			bool Contains(FNVHash id);
			object Get(FNVHash id);
			void Add(FNVHash id, object item);
			IEnumerable<FNVHash> GetAllIds();
		}
		private class Db<T> : IDb {

			public FNVHash Id { get; }
			public string Name { get; }
			public int Count { get { return m_items.Count; } }

			Dictionary<FNVHash, T> m_items = new Dictionary<FNVHash, T>();

			public Db(string name) {
				Name = name;
				Id = name;
			}

			public bool Contains(FNVHash hash) {
				return m_items.ContainsKey(hash);
			}
			public object Get(FNVHash id) {
				if (m_items.TryGetValue(id, out T value)) {
					return value;
				} else {
					throw new KeyNotFoundException(string.Format(
						"No item with id `{0}' exists in Database `{1}'.",
						id, Name
					));
				}
			}
			public void Add(FNVHash id, object item) {
				if (item is T obj) {
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
						Name, id, item.GetType().Name, typeof(T).Name
					));
				}
			}
			public IEnumerable<FNVHash> GetAllIds() {
				foreach (FNVHash key in m_items.Keys) {
					yield return key;
				}
			}

		}

		#endregion

		private Dictionary<FNVHash, IDb> m_database = new Dictionary<FNVHash, IDb>();

		public void Add<T>(string db, IEnumerable<T> dataset, Func<T,FNVHash> hashGetter) {
			if (!m_database.ContainsKey(db)) {
				Db<T> database = new Db<T>(db);
				foreach (T item in dataset) {
					database.Add(hashGetter(item), item);
				}
			} else {
				throw new Exception(string.Format(
					"Database with name `{0}' already exists", db
				));
			}
		}
		public void Remove(string db) {
			m_database.Remove(db);
		}

		public void Clear() {
			m_database.Clear();
		}

		public T Get<T>(string db, FNVHash id) {
			if (m_database.ContainsKey(db)) {
				return (T)m_database[db].Get(id);
			} else {
				throw new KeyNotFoundException(string.Format(
					"No Database with name `{0}' exists", db
				));
			}
		}
		public bool Contains(string db) {
			return m_database.ContainsKey(db);
		}
		public bool Contains(string db, FNVHash id) {
			if (m_database.ContainsKey(db)) {
				return m_database[db].Contains(id);
			} else {
				return false;
			}
		}
		public IEnumerable<FNVHash> GetAllIds(string db) {
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