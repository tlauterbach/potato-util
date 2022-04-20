using System.Collections.Generic;

namespace PotatoUtil {

	/// <summary>
	/// Used by FNVHash to get a recorded string value
	/// if the system is enabled
	/// </summary>
	public static class HashLookup {

		private static bool m_enabled = true;
		private static Dictionary<FNVHash, string> m_table = new Dictionary<FNVHash, string>();

		public static bool Enabled {
			get { return m_enabled; }
			set { 
				m_enabled = value; 
				if (!m_enabled) {
					m_table.Clear();
				}
			}
		}

		public static void Register(FNVHash hash, string value) {
			if (!m_enabled || m_table.ContainsKey(hash)) {
				return;
			}
			m_table.Add(hash, value);
		}

		public static void Clear() {
			m_table.Clear();
		}

		public static string ToString(FNVHash hash) {
			if (m_table.ContainsKey(hash)) {
				return m_table[hash];
			} else {
				return ((uint)hash).ToString();
			}
		}
	}

}