using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace PotatoUtil {

	[Serializable]
	public struct MinMax : IEquatable<MinMax> {

		public float Min {
			get { return m_min; }
		}
		public float Max {
			get { return m_max; }
		}

		[SerializeField]
		private float m_min;
		[SerializeField]
		private float m_max;

		public MinMax(float min, float max) {
			m_min = min;
			m_max = max;
		}
		public bool Equals(MinMax other) {
			return m_min == other.m_min && m_max == other.m_max;
		}

		public float Clamp(float value) {
			return Mathf.Clamp(value, m_min, m_max);
		}

		#if UNITY_EDITOR
		[CustomPropertyDrawer(typeof(MinMax))]
		private class Drawer : PropertyDrawer {

			public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
				position = EditorGUI.PrefixLabel(position, label);
				SerializedProperty min = property.FindPropertyRelative("m_min");
				SerializedProperty max = property.FindPropertyRelative("m_max");
				
				Rect minBox = position;
				minBox.width /= 2f;
				minBox.width -= 4f;
				DrawProperty(minBox, min, new GUIContent("Min"));

				Rect maxBox = position;
				maxBox.width = maxBox.width / 2 - 4f;
				maxBox.x += minBox.width + 8f;
				DrawProperty(maxBox, max, new GUIContent("Max"));

				property.serializedObject.ApplyModifiedProperties();

			}
			private Rect DrawLabel(GUIContent label, Rect position) {
				Rect labelRect = position;
				labelRect.width = 32f;
				EditorGUI.LabelField(labelRect, label);
				position.x += 32f;
				position.width -= 32f;
				return position;
			}
			private void DrawProperty(Rect position, SerializedProperty property, GUIContent label) {
				position = DrawLabel(label, position);
				float newValue = EditorGUI.FloatField(position, property.floatValue);
				if (newValue != property.floatValue) {
					property.floatValue = newValue;
				}
			}

			public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
				return EditorGUIUtility.singleLineHeight;
			}

		}

		#endif



	}


}