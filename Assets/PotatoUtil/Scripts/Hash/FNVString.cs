using System;

namespace PotatoUtil {

	/// <summary>
	/// Version of the FNVHash that maintains and can
	/// serialize the string value originally given
	/// to the struct.
	/// </summary>
	public struct FNVString : IEquatable<FNVString>, IEquatable<FNVHash> {

		public static readonly FNVString Empty = new FNVString();

		private string m_string;
		private FNVHash m_hash;

		public FNVString(string str) {
			m_string = str;
			m_hash = new FNVHash(str);
		}

		public static bool operator ==(FNVString lhs, FNVString rhs) {
			return lhs.Equals(rhs);
		}
		public static bool operator !=(FNVString lhs, FNVString rhs) {
			return !lhs.Equals(rhs);
		}
		public static implicit operator FNVHash(FNVString str) {
			return str.m_hash;
		}
		public static implicit operator FNVString(string str) {
			return new FNVString(str);
		}

		public bool Equals(FNVString other) {
			return m_hash == other.m_hash;
		}
		public bool Equals(FNVHash other) {
			return m_hash == other;
		}
		public override bool Equals(object obj) {
			if (obj is FNVString str) {
				return Equals(str);
			} else if (obj is FNVHash hash) {
				return Equals(hash);
			} else {
				return false;
			}
		}
		public override int GetHashCode() {
			return m_hash;
		}
		public override string ToString() {
			return m_string;
		}
	}
}


