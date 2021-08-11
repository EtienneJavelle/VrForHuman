using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad, ExecuteAlways]
public static class DefaultSceneLoader {
    private const string keySceneToLoadBack = "SceneToLoadBack";

    static DefaultSceneLoader() {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    private static void LoadDefaultScene(PlayModeStateChange state) {
        if(state == PlayModeStateChange.ExitingEditMode) {
            Scene currentScene = EditorSceneManager.GetActiveScene();
            string defaultScenePath = EditorBuildSettings.scenes[0].path;
            if(currentScene.path != defaultScenePath) {
                if(EditorUtility.DisplayDialog("Default Scene Loader Is Active", "Load first build scene?", "Yes", "No")) {
                    PlayerPrefs.SetString(keySceneToLoadBack, currentScene.path);
                    EditorSceneManager.OpenScene(defaultScenePath, OpenSceneMode.Single);
                }
            } else {
                PlayerPrefs.DeleteKey(keySceneToLoadBack);
            }
        }
        if(state == PlayModeStateChange.EnteredEditMode) {
            if(PlayerPrefs.HasKey(keySceneToLoadBack)) {
                string sceneToLoadBack = PlayerPrefs.GetString(keySceneToLoadBack);
                EditorSceneManager.OpenScene(sceneToLoadBack, OpenSceneMode.Single);
                PlayerPrefs.DeleteKey(keySceneToLoadBack);
            }
        }
    }
}