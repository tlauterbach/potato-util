using PotatoSerializer;
using PcgRandom;
using System;

namespace PotatoUtil {

	/// <summary>
	/// PotatoSerializer compatible wrapper of 
	/// the Pcg randomizer.
	/// </summary>
	public class Randomizer : Pcg, ISerialObject {

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

		public void Serialize(ISerializer ioSerializer) {
			ioSerializer.Serialize("inc", ref m_inc);
			ioSerializer.Serialize("state", ref m_state);
		}

		public override string ToString() {
			return string.Format("{0}-{1}", 
				Convert.ToBase64String(BitConverter.GetBytes(m_inc)).Replace("=",""), 
				Convert.ToBase64String(BitConverter.GetBytes(m_state)).Replace("=","")
			);
		}

	}

}