using UnityEditor;
using UnityEngine;

namespace CardiacMassage {
    [CustomPropertyDrawer(typeof(Rank))]
    public class RankDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty text = property.FindPropertyRelative("text");
            SerializedProperty points = property.FindPropertyRelative("points");
            SerializedProperty offset = property.FindPropertyRelative("offset");
            SerializedProperty colors = property.FindPropertyRelative("colors");

            float quarter = position.width / 4;
            float rankHeight = EditorGUIUtility.singleLineHeight;
            Rect textPosition = new Rect(position.x, position.y, quarter * 1.5f, rankHeight);
            Rect pointPosition = new Rect(position.x + quarter * 1.5f, position.y, quarter * 1.5f, rankHeight);
            Rect valuePosition = new Rect(position.x + (quarter * 3), position.y, quarter, rankHeight);
            Rect clipPosition = new Rect(position) {
                y = position.y + rankHeight + 2,
                height = rankHeight,
            };
            Rect colorsPosition = new Rect(position) {
                y = clipPosition.y + rankHeight + 2,
                height = rankHeight * 2,
            };

            EditorGUI.BeginProperty(position, label, property);
            text.stringValue = EditorGUI.TextField(textPosition, text.stringValue);
            float old = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = valuePosition.width / 2;
            points.intValue = EditorGUI.IntField(pointPosition, new GUIContent("Points"), points.intValue);
            offset.floatValue = EditorGUI.FloatField(valuePosition, new GUIContent("Offset"), offset.floatValue);
            EditorGUIUtility.labelWidth = old;
            EditorGUI.PropertyField(clipPosition, property.FindPropertyRelative("clip"));
            EditorGUI.PropertyField(colorsPosition, colors);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if(!EditorGUIUtility.wideMode) {
                return EditorGUIUtility.singleLineHeight * 6 + 12;
            }
            return EditorGUIUtility.singleLineHeight * 4 + 10;
        }
    }
}