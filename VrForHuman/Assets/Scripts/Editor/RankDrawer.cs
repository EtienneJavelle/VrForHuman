using UnityEditor;
using UnityEngine;

namespace CardiacMassage {
    [CustomPropertyDrawer(typeof(Rank))]
    public class RankDrawer : PropertyDrawer {
        private bool isWindowTooSmall => EditorGUIUtility.currentViewWidth < 435;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty text = property.FindPropertyRelative("text");
            SerializedProperty points = property.FindPropertyRelative("points");
            SerializedProperty offset = property.FindPropertyRelative("offset");
            SerializedProperty colors = property.FindPropertyRelative("colors");
            SerializedProperty colorTL = colors.FindPropertyRelative("topLeft");
            SerializedProperty colorTR = colors.FindPropertyRelative("topRight");
            SerializedProperty colorBL = colors.FindPropertyRelative("bottomLeft");
            SerializedProperty colorBR = colors.FindPropertyRelative("bottomRight");

            float quarter = position.width / 4;
            float rankHeight = EditorGUIUtility.singleLineHeight;
            Rect textPosition = new Rect(position.x, position.y, quarter * 1.5f, rankHeight);
            Rect pointPosition = new Rect(position.x + quarter * 1.5f, position.y, quarter * 1.5f, rankHeight);
            Rect valuePosition = new Rect(position.x + (quarter * 3), position.y, quarter, rankHeight);
            Rect colorsPosition = new Rect(position) {
                y = position.y + rankHeight + 2,
                height = rankHeight * 2,
            };

            EditorGUI.BeginProperty(position, label, property);
            text.stringValue = EditorGUI.TextField(textPosition, text.stringValue);
            float old = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = valuePosition.width / 2;
            points.intValue = EditorGUI.IntField(pointPosition, new GUIContent("Points"), points.intValue);
            offset.floatValue = EditorGUI.FloatField(valuePosition, new GUIContent("Offset"), offset.floatValue);
            EditorGUIUtility.labelWidth = old;

            EditorGUI.PropertyField(colorsPosition, colors);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if(isWindowTooSmall) {
                return EditorGUIUtility.singleLineHeight * 5 + 8;
            }
            return EditorGUIUtility.singleLineHeight * 3 + 4;
        }
    }
}