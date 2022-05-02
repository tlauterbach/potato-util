using UnityEditor;
using UnityEngine;

namespace PotatoUtil.Editor {

	[CustomPropertyDrawer(typeof(WrappedAttribute))]
	public class WrappedAttributeDrawer : PropertyDrawer {

		private static GUIStyle m_style;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			WrappedAttribute attr = attribute as WrappedAttribute;
			if (attr == null) {
				base.OnGUI(position, property, label);
				return;
			}
			if (m_style == null) {
				m_style = new GUIStyle(GUI.skin.textField);
				m_style.wordWrap = true;
			}
			position = EditorGUI.PrefixLabel(position, label);
			string value = property.stringValue;
			property.stringValue = EditorGUI.TextArea(position, property.stringValue, m_style);
			if (value != property.stringValue) {
				property.serializedObject.ApplyModifiedProperties();
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			WrappedAttribute attr = attribute as WrappedAttribute;
			if (attr == null) {
				return base.GetPropertyHeight(property, label);
			} else {
				return attr.Height;
			}
		}


	}

}