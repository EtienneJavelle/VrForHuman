using UnityEditor;

public class DialogWindow : EditorWindow {
    private SerializedProperty property;
    public static void Init(SerializedProperty property) {
        DialogWindow window = (DialogWindow)EditorWindow.GetWindow(typeof(DialogWindow), true);
        window.property = property;
        window.Show();
    }

    private void OnGUI() {
        if(property == null) {
            return;
        }

        EditorGUILayout.PropertyField(property.FindPropertyRelative("texts"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("dialogSound"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("OnCompleted"));
    }
}
