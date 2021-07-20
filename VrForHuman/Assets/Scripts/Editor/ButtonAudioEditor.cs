using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(ButtonAudio))]
public class ButtonAudioEditor : EtienneEditor.Editor<ButtonAudioEditor> {
    private bool doesUIAudioManagerExists;
    private void OnEnable() {
        FindUIAudioManager();
    }

    private void OnValidate() {
        FindUIAudioManager();
    }

    private UIAudioManager FindUIAudioManager(bool includeInactive = false) {
        UIAudioManager uiAudioManager = GameObject.FindObjectOfType<UIAudioManager>(includeInactive);
        doesUIAudioManagerExists = uiAudioManager != null;
        return uiAudioManager;
    }

    public override void OnInspectorGUI() {
        if(!doesUIAudioManagerExists) {
            EditorGUILayout.HelpBox("There is no UIAudioManager, this component works in pair with UIAudioManager and therefore does not work without. Click the button below to create a UIAudioManager. Or click the second button to fetch for an inactive UIAudioManager.", MessageType.Error);
            if(GUILayout.Button("Create UIAudioManager")) {
                CreateAnUIAudioManager();
            }
            if(GUILayout.Button("Fetch for an inactive UIAudioManager and enable it")) {
                UIAudioManager uiAudioManager = FindUIAudioManager(true);
                if(uiAudioManager != null) {
                    uiAudioManager.enabled = true;
                    uiAudioManager.gameObject.SetActive(true);
                    EditorGUIUtility.PingObject(uiAudioManager);
                } else {
                    Debug.LogWarning("Did not find any UIAudioManager but created one instead");
                    CreateAnUIAudioManager();
                }
            }
        }
        base.OnInspectorGUI();
    }

    private void CreateAnUIAudioManager() {
        GameObject go = new GameObject("UIAudioManager");
        go.AddComponent<UIAudioManager>();
        EditorGUIUtility.PingObject(go);
        doesUIAudioManagerExists = true;
    }
}
