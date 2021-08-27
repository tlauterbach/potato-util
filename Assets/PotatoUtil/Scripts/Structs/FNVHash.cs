using BeauData;
using System;
using UnityEngine;

namespace PotatoUtil {

	/// <summary>
	/// A 32 bit hash for strings generated using the 
	/// Fowler-Noll-Vo hashing algorthim.
	/// http://www.isthe.com/chongo/tech/comp/fnv/
	/// </summary>
	[Serializable]
	public struct FNVHash : IEquatable<FNVHash>, IEquatable<int>, IEquatable<uint>, ISerializedProxy<uint> {

		public static readonly FNVHash Empty = new FNVHash();

		[SerializeField]
		private uint m_hash;

		private const uint PRIME = 16777619u;
		private const uint OFFSET = 2166136261u;

		public FNVHash(string str) {
			m_hash = OFFSET;
			foreach (char c in str) {
				m_hash ^= c;
				m_hash *= PRIME;
			}
		}
		public FNVHash(SubString str) {
			m_hash = OFFSET;
			foreach (char c in str) {
				m_hash ^= c;
				m_hash *= PRIME;
			}
		}
		public FNVHash(int integer) {
			m_hash = (uint)integer;
		}
		public FNVHash(uint integer) {
			m_hash = integer;
		}

		public static bool operator ==(FNVHash lhs, FNVHash rhs) {
			return lhs.Equals(rhs);
		}
		public static bool operator !=(FNVHash lhs, FNVHash rhs) {
			return !lhs.Equals(rhs);
		}
		public static implicit operator uint(FNVHash obj) {
			return obj.m_hash;
		}
		public static implicit operator int(FNVHash obj) {
			return unchecked((int)obj.m_hash);
		}
		public static implicit operator FNVHash(string value) {
			return new FNVHash(value);
		}
		public static implicit operator FNVHash(uint value) {
			return new FNVHash(value);
		}
		public static implicit operator FNVHash(int value) {
			return new FNVHash(value);
		}

		public override bool Equals(object obj) {
			if (obj == null || !(obj is FNVHash)) {
				return false;
			}
			return Equals((FNVHash)obj);
		}
		public bool Equals(uint other) {
			return m_hash == other;
		}
		public bool Equals(int other) {
			return GetHashCode() == other;
		}
		public bool Equals(FNVHash other) {
			return m_hash == other.m_hash;
		}
		public override int GetHashCode() {
			return (int)m_hash;
		}
		public override string ToString() {
			return m_hash.ToString();
		}

		public uint GetProxyValue(ISerializerContext inContext) {
			return m_hash;
		}

		public void SetProxyValue(uint inValue, ISerializerContext inContext) {
			m_hash = inValue;
		}
	}


}