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
            Rect ColorsLabelPosition = new Rect(position.x, position.y + rankHeight + 2, EditorGUIUtility.labelWidth, rankHeight * 2);
            Rect colorPosition = new Rect(position.x + EditorGUIUtility.labelWidth,
                position.y + rankHeight + 2,
                (position.width - EditorGUIUtility.labelWidth) / 2,
                rankHeight);

            EditorGUI.BeginProperty(position, label, property);
            text.stringValue = EditorGUI.TextField(textPosition, text.stringValue);
            float old = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = valuePosition.width / 2;
            points.intValue = EditorGUI.IntField(pointPosition, new GUIContent("Points"), points.intValue);
            offset.floatValue = EditorGUI.FloatField(valuePosition, new GUIContent("Offset"), offset.floatValue);
            EditorGUIUtility.labelWidth = old;

            EditorGUI.PrefixLabel(ColorsLabelPosition, new GUIContent("Colors"));
            DrawColor(colorTL, colorPosition);
            colorPosition.x += colorPosition.width;
            DrawColor(colorTR, colorPosition);
            if(isWindowTooSmall) {
                colorPosition.y += colorPosition.height * 2 + 4;
            } else {
                colorPosition.y += colorPosition.height + 2;
            }
            DrawColor(colorBR, colorPosition);
            colorPosition.x -= colorPosition.width;
            DrawColor(colorBL, colorPosition);
            EditorGUI.EndProperty();
        }

        private void DrawColor(SerializedProperty color, Rect position) {
            float maxWidth = position.width;
            Rect colorPosition = position;
            Rect textPosition = position;
            if(isWindowTooSmall) {
                textPosition.y += textPosition.height + 2;
            } else {
                colorPosition.width = 50;
                textPosition.x += colorPosition.width;
                textPosition.width = Mathf.Min(maxWidth - colorPosition.width - 2, 100);
            }

            EditorGUI.BeginChangeCheck();
            Color colorvalue = EditorGUI.ColorField(colorPosition, color.colorValue);
            if(EditorGUI.EndChangeCheck()) {
                color.colorValue = colorvalue;
            }

            string colorInHexa = ColorUtility.ToHtmlStringRGBA(color.colorValue);
            EditorGUI.BeginChangeCheck();
            string userEnteredColor = EditorGUI.TextArea(textPosition, $"#{colorInHexa}");
            if(EditorGUI.EndChangeCheck()) {
                ColorUtility.TryParseHtmlString(userEnteredColor, out Color convertedColor);
                color.colorValue = convertedColor;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if(isWindowTooSmall) {
                return EditorGUIUtility.singleLineHeight * 5 + 8;
            }
            return EditorGUIUtility.singleLineHeight * 3 + 4;
        }
    }
}