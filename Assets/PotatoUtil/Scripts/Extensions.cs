using System;
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

		public static void Shuffle<T>(this IList<T> list, Randomizer randomizer) {
			if (randomizer == null) {
				throw new ArgumentNullException(nameof(randomizer));
			}
			int n = list.Count;
			while (n > 0) {
				int rand = Convert.ToInt32(randomizer.Range32(Convert.ToUInt32(n--)));
				T item = list[rand];
				list[rand] = list[n];
				list[n] = item;
			}
		}
		public static void Shuffle<T>(this T[] array, Randomizer randomizer) {
			if (randomizer == null) {
				throw new ArgumentNullException(nameof(randomizer));
			}
			int n = array.Length;
			while (n > 0) {
				int rand = Convert.ToInt32(randomizer.Range32(Convert.ToUInt32(n--)));
				T item = array[rand];
				array[rand] = array[n];
				array[n] = item;
			}
		}

		public static T Choose<T>(this IList<T> list, Randomizer randomizer) {
			if (randomizer == null) {
				throw new ArgumentNullException(nameof(randomizer));
			}
			if (list.Count == 0) {
				throw new InvalidOperationException("Given IList contains no elements, so no element can be returned.");
			}
			return list[Convert.ToInt32(randomizer.Range32(Convert.ToUInt32(list.Count)))];
		}
		public static T Choose<T>(this T[] array, Randomizer randomizer) {
			if (randomizer == null) {
				throw new ArgumentNullException(nameof(randomizer));
			}
			if (array.Length == 0) {
				throw new InvalidOperationException("Given Array has a length of zero, so no element can be returned.");
			}
			return array[Convert.ToInt32(randomizer.Range32(Convert.ToUInt32(array.Length)))];
		}
		public static T Take<T>(this IList<T> list, Randomizer randomizer) {
			if (randomizer == null) {
				throw new ArgumentNullException(nameof(randomizer));
			}
			if (list.Count == 0) {
				throw new InvalidOperationException("Given IList contains no elements, so no element can be returned.");
			}
			T item = Choose(list, randomizer);
			list.Remove(item);
			return item;
		}


	}
}


