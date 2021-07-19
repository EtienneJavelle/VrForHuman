using UnityEditor;
using UnityEngine;

namespace TMPro {
    [CustomPropertyDrawer(typeof(VertexGradient))]
    public class VertexGradientDrawer : PropertyDrawer {
        private bool IsWindowTooSmall => EditorGUIUtility.currentViewWidth < 435;
        private float SingleLineHeight => EditorGUIUtility.singleLineHeight + 2;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty[] colors = new SerializedProperty[]{
            property.FindPropertyRelative("topLeft"),
            property.FindPropertyRelative("topRight"),
            property.FindPropertyRelative("bottomLeft"),
            property.FindPropertyRelative("bottomRight")
            };
            Rect colorsLabelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, SingleLineHeight);
            Rect colorPosition = new Rect(position.x + EditorGUIUtility.labelWidth,
                  position.y,
                  (position.width - EditorGUIUtility.labelWidth) / 2 - 2,
                  EditorGUIUtility.singleLineHeight);
            Rect[] colorPositions = new Rect[] {
                colorPosition,
                new Rect(colorPosition){
                    x = colorPosition.x + colorPosition.width + 2,
                },
                new Rect(colorPosition){
                    y = colorPosition.y + SingleLineHeight * (IsWindowTooSmall?2:1),
                },
                new Rect(colorPosition){
                    x = colorPosition.x + colorPosition.width + 2,
                    y = colorPosition.y + SingleLineHeight * (IsWindowTooSmall?2:1),
                },
            };

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PrefixLabel(colorsLabelPosition, label);
            for(int i = 0; i < colors.Length; i++) {
                DrawColor(colors[i], colorPositions[i]);
            }
            EditorGUI.EndProperty();
        }

        private void DrawColor(SerializedProperty color, Rect position) {
            float maxWidth = position.width;
            Rect colorPosition = position;
            Rect textPosition = position;
            if(IsWindowTooSmall) {
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
            if(IsWindowTooSmall) {
                return SingleLineHeight * 4;
            }
            return SingleLineHeight * 2;
        }
    }
}