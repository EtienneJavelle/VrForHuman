using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad, ExecuteAlways]
public static class DefaultSceneLoader {
    static DefaultSceneLoader() {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    private static void LoadDefaultScene(PlayModeStateChange state) {
        if(state == PlayModeStateChange.EnteredPlayMode) {
            if(EditorUtility.DisplayDialog("Default Scene Loader Is Active", "Load first build scene?", "Yes", "No")) {
                EditorSceneManager.LoadScene(0);
            }
        }
    }
}