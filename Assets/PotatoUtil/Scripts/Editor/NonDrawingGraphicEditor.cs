/// @creator: Slipp Douglas Thompson
/// @license: Public Domain per The Unlicense.  See <http://unlicense.org/>.
/// @purpose: Slimmed-down Inspector UI for `NonDrawingGraphic` class.
/// @why: Because this functionality should be built-into Unity.
/// @usage: Add a `NonDrawingGraphic` component to the GameObject you want clickable, but without its own image/graphics.
/// @interwebsouce: https://gist.github.com/capnslipp/349c18283f2fea316369

using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using PotatoUtil;

namespace PotatoUtil.Editor {
	
	[CanEditMultipleObjects, CustomEditor(typeof(NonDrawingGraphic), false)]
	public class NonDrawingGraphicEditor : GraphicEditor {
		public override void OnInspectorGUI() {
			base.serializedObject.Update();
			EditorGUILayout.PropertyField(base.m_Script, new GUILayoutOption[0]);
			// skipping AppearanceControlsGUI
			base.RaycastControlsGUI();
			base.serializedObject.ApplyModifiedProperties();
		}
	}

}
