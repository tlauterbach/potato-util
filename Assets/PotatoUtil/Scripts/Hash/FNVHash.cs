﻿using System;
using System.Text;

namespace PotatoUtil {

	/// <summary>
	/// A 32 bit hash for strings generated using the 
	/// Fowler-Noll-Vo hashing algorthim.
	/// http://www.isthe.com/chongo/tech/comp/fnv/
	/// </summary>
	public struct FNVHash : IEquatable<FNVHash>, IEquatable<int>, IEquatable<uint> {

		public static readonly FNVHash Empty = new FNVHash();

		private uint m_hash;

		private const uint PRIME = 16777619u;
		private const uint OFFSET = 2166136261u;

		public FNVHash(string str) {
			m_hash = OFFSET;
			foreach (char c in str) {
				m_hash ^= c;
				m_hash *= PRIME;
			}
			HashLookup.Register(this, str);
		}
		public FNVHash(StringBuilder builder) {
			m_hash = OFFSET;
			int ix = 0;
			while (ix < builder.Length) {
				m_hash ^= builder[ix];
				m_hash *= PRIME;
			}
			HashLookup.Register(this, builder);
		}

		public FNVHash(SubString str) {
			m_hash = OFFSET;
			foreach (char c in str) {
				m_hash ^= c;
				m_hash *= PRIME;
			}
			HashLookup.Register(this, str.ToString());
		}
		public FNVHash(uint hash) {
			m_hash = hash;
		}
		public FNVHash(int hash) {
			m_hash = (uint)hash;
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
			if (obj is FNVHash hash) {
				return Equals(hash);
			} else if (obj is int integer) {
				return Equals(integer);
			} else if (obj is uint unsigned) {
				return Equals(unsigned);
			} else {
				return false;
			}
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
			return HashLookup.ToString(this);
		}

	}

}