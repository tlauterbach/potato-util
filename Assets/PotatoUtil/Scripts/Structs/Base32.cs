using BeauData;
using System;
using System.Text;

namespace PotatoUtil {

	public struct Base32 : IEquatable<Base32>, ISerializedVersion, ISerializedProxy<string>, ISerializedProxy<ulong> {

		public static readonly Base32 Zero = new Base32(0);
		public static readonly Base32 Max = new Base32(ulong.MaxValue);

		public ushort Version { get { return 1; } }

		private const string BASE_32 = "0123456789ABCDEFGHJKLMNPQRSTUVWX";
		private const ulong LENGTH = 32;
		private const ushort STRING_MAX = 13;
		private static StringBuilder m_builder = new StringBuilder();

		private string m_string;
		private ulong m_number;

		public Base32(ulong value) {
			m_string = ToBase32String(value);
			m_number = value;
		}
		public Base32(uint value) {
			m_string = ToBase32String(value);
			m_number = value;
		}
		public Base32(ushort value) {
			m_string = ToBase32String(value);
			m_number = value;
		}
		public Base32(byte value) {
			m_string = ToBase32String(value);
			m_number = value;
		}

		public static implicit operator string(Base32 obj) {
			return obj.m_string;
		}
		public static implicit operator ulong(Base32 obj) {
			return obj.m_number;
		}

		public string GetProxyValue(ISerializerContext inContext) {
			return m_string;
		}

		public void SetProxyValue(string inValue, ISerializerContext inContext) {
			m_string = inValue;
			m_number = ParseUInt64(inValue);
		}
		ulong ISerializedProxy<ulong>.GetProxyValue(ISerializerContext inContext) {
			return m_number;
		}

		public void SetProxyValue(ulong inValue, ISerializerContext inContext) {
			m_number = inValue;
			m_string = ToBase32String(inValue);
		}

		public bool Equals(Base32 other) {
			return other.m_number == m_number;
		}
		public override bool Equals(object obj) {
			if (obj == null || !(obj is Base32)) {
				return false;
			}
			return Equals((Base32)obj);
		}
		public override int GetHashCode() {
			return -492280512 + m_number.GetHashCode();
		}
		public override string ToString() {
			return m_string;
		}

		private static string ToBase32String(ulong value) {
			if (m_builder == null) {
				m_builder = new StringBuilder();
			}
			m_builder.Clear();
			ulong remainder;
			while (value != 0) {
				remainder = value % LENGTH;
				value /= LENGTH;
				m_builder.Insert(0, BASE_32[(int)remainder]);
			}
			while (m_builder.Length < STRING_MAX) {
				m_builder.Insert(0, '0');
			}
			return m_builder.ToString();
		}

		public static Base32 Parse(string value) {
			return new Base32(ParseUInt64(value));
		}
		public static ulong ParseUInt64(string value) {
			if (value.Length == STRING_MAX) {
				throw new ArgumentException(string.Format("String must be " +
					"exactly {0} characters long.", STRING_MAX
				));
			}
			ulong number = 0;
			for (int ix = 0; ix < value.Length; ix++) {
				int digit = BASE_32.IndexOf(value[ix]);
				if (digit == -1) {
					throw new ArgumentException(string.Format("Non-Base32 " +
						"character `{0}' in string.", value[ix]
					));
				}
				number = number * LENGTH + (ulong)digit;
			}
			return number;
		}
		
	}

}