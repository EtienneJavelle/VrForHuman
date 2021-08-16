using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Dialog))]
public class DialogDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        position.height = EditorGUIUtility.singleLineHeight + 1;
        label.text = $"Open dialog {label.text.Substring(label.text.Length - 1)}";
        EditorGUI.BeginProperty(position, label, property);
        if(GUI.Button(position, label)) {
            DialogWindow.Init(property);
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUIUtility.singleLineHeight + 2;
    }
}
