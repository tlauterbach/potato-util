using System.Collections.Generic;

namespace PotatoUtil {

	public static class Extensions {

		public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> items) {
			foreach (T item in items) {
				set.Add(item);
			}
		}
		public static void RemoveRange<T>(this HashSet<T> set, IEnumerable<T> items) {
			foreach (T item in items) {
				set.Remove(item);
			}
		}
		public static void RemoveRange<T>(this IList<T> set, IEnumerable<T> items) {
			foreach (T item in items) {
				set.Remove(item);
			}
		}

	}
}


