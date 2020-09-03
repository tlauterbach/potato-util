using BeauData;
using PcgRandom;
using System;
using UnityEngine;

namespace PotatoUtil {

	/// <summary>
	/// BeauData serialization-compatible wrapper of 
	/// the Pcg randomizer.
	/// </summary>
	public class Randomizer : Pcg, ISerializedObject, ISerializedVersion, ISerializedProxy<string> {

		public ushort Version { get { return 1; } }

		public Randomizer() : base() { }
		public Randomizer(ulong initState, ulong initSeq) : base(initState, initSeq) { }

		public Randomizer(DateTime seedTime) {
			// sort of scrambles the fairly
			// homogenous year/month portion
			// with the more variable seconds
			ulong seq = (ulong)seedTime.Ticks;
			seq = (seq << 32 | seq >> 32) ^ seq;
			Seed(~(ulong)seedTime.Ticks, seq);
		}

		public void Serialize(Serializer ioSerializer) {
			ioSerializer.Serialize("inc", ref m_inc);
			ioSerializer.Serialize("state", ref m_state);
		}

		public string GetProxyValue(ISerializerContext inContext) {
			return ToString();
		}

		public void SetProxyValue(string inValue, ISerializerContext inContext) {
			string[] split = inValue.Split('-');
			if (split.Length != 2 || split[0].Length != 13 || split[1].Length != 13) {
				throw new ArgumentException(string.Format("String `{0}' cannot be " +
					"converted to a Randomizer state!", inValue), nameof(inValue)
				);
			}
			m_inc = Base32.ParseUInt64(split[0]);
			m_state = Base32.ParseUInt64(split[1]);
		}

		public override string ToString() {
			return string.Format("{0}-{1}", new Base32(m_inc), new Base32(m_state));
		}

	}

}