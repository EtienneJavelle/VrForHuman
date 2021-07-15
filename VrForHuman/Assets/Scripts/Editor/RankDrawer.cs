using UnityEditor;
using UnityEngine;

namespace CardiacMassage {
    [CustomPropertyDrawer(typeof(Rank))]
    public class RanksDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            //base.OnGUI(position, property, label);
            SerializedProperty displayName = property.FindPropertyRelative("displayName");
            SerializedProperty points = property.FindPropertyRelative("points");
            SerializedProperty offset = property.FindPropertyRelative("offset");

            EditorGUI.BeginProperty(position, label, property);
            position.height -= 1;
            position.width /= 3;
            position.width /= 2;

            EditorGUI.LabelField(position, "Name");
            position.x += position.width - 8;
            position.width += 10;
            displayName.stringValue = EditorGUI.TextField(position, displayName.stringValue);
            position.width -= 10;

            position.x += position.width + 12;
            EditorGUI.LabelField(position, "Points");
            position.x += position.width - 6;
            points.floatValue = EditorGUI.FloatField(position, points.floatValue);

            position.x += position.width + 2;
            EditorGUI.LabelField(position, "Offset");
            position.x += position.width - 2;
            offset.floatValue = EditorGUI.FloatField(position, offset.floatValue);
            EditorGUI.EndProperty();
        }
    }
}