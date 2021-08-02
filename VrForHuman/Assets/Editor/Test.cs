using UnityEditor;

public class MaskFieldExample : EditorWindow {
    private static int flags = 0;
    private static string[] options = new string[] { "CanJump", "CanShoot", "CanSwim" };

    [MenuItem("Examples/Mask Field usage")]
    private static void Init() {
        MaskFieldExample window = (MaskFieldExample)GetWindow(typeof(MaskFieldExample));
        window.Show();
    }

    private void OnGUI() {
        flags = EditorGUILayout.MaskField("Player Flags", flags, options);
        EditorGUILayout.SelectableLabel(flags.ToString());
    }
}