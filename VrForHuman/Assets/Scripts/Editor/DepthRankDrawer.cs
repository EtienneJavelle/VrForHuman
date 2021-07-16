using UnityEditor;
using UnityEngine;

namespace CardiacMassage {
    [CustomPropertyDrawer(typeof(DepthRank))]
    public class DepthRankDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty text = property.FindPropertyRelative("text");
            SerializedProperty value = property.FindPropertyRelative("value");
            SerializedProperty colors = property.FindPropertyRelative("colors");
            SerializedProperty colorTL = colors.FindPropertyRelative("topLeft");
            SerializedProperty colorTR = colors.FindPropertyRelative("topRight");
            SerializedProperty colorBL = colors.FindPropertyRelative("bottomLeft");
            SerializedProperty colorBR = colors.FindPropertyRelative("bottomRight");

            float quarter = position.width / 4;
            float rankHeight = EditorGUIUtility.singleLineHeight;
            Rect textPosition = new Rect(position.x, position.y, quarter * 3, rankHeight);
            Rect valuePosition = new Rect(position.x + (quarter * 3), position.y, quarter, rankHeight);
            Rect ColorsLabelPosition = new Rect(position.x, position.y + rankHeight, EditorGUIUtility.labelWidth, position.height);
            Rect colorPosition = new Rect(position.x + EditorGUIUtility.labelWidth,
                position.y + rankHeight,
                (position.width - EditorGUIUtility.labelWidth) / 2,
                (position.height - rankHeight) / 2);

            EditorGUI.BeginProperty(position, label, property);
            text.stringValue = EditorGUI.TextField(textPosition, text.stringValue);
            float old = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = valuePosition.width / 2;
            value.floatValue = EditorGUI.FloatField(valuePosition, new GUIContent("Value"), value.floatValue);
            EditorGUIUtility.labelWidth = old;

            EditorGUI.PrefixLabel(ColorsLabelPosition, new GUIContent("Colors"));
            DrawColor(colorTL, colorPosition);
            colorPosition.x += colorPosition.width;
            DrawColor(colorTR, colorPosition);
            colorPosition.y += colorPosition.height;
            DrawColor(colorBR, colorPosition);
            colorPosition.x -= colorPosition.width;
            DrawColor(colorBL, colorPosition);
            EditorGUI.EndProperty();
        }

        private static void DrawColor(SerializedProperty colorTL, Rect position) {
            float maxWidth = position.width;
            EditorGUI.BeginChangeCheck();
            position.width = 50;
            Color colorvalue = EditorGUI.ColorField(position, colorTL.colorValue);
            if(EditorGUI.EndChangeCheck()) {
                colorTL.colorValue = colorvalue;
            }
            position.x += position.width;
            position.width = maxWidth - 50;
            string colorInHexa = ColorUtility.ToHtmlStringRGBA(colorTL.colorValue);
            EditorGUI.BeginChangeCheck();
            string userEnteredColor = EditorGUI.TextArea(position, $"#{colorInHexa}");
            if(EditorGUI.EndChangeCheck()) {
                ColorUtility.TryParseHtmlString(userEnteredColor, out Color convertedColor);
                colorTL.colorValue = convertedColor;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight * 3;
        }
    }
}