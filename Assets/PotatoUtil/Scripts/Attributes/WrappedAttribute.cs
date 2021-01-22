using UnityEngine;

namespace PotatoUtil {

	/// <summary>
	/// Wraps string fields with a fixed height
	/// </summary>
	public sealed class WrappedAttribute : PropertyAttribute {

		public float Height { get; set; }

		public WrappedAttribute(float height = 48f) {
			Height = height;
		}

	}

}

