#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class EndMenu : MonoBehaviour {
    [SerializeField] private ButtonUI menuButton, quitButton;

    private void Start() {
        menuButton.onHandClick.AddListener(_ => {
            SceneLoader.Instance.ChangeScene(Scenes.MainMenu);
            Debug.Log("MenuButton");
        }
        );

        quitButton.onHandClick.AddListener(_ => {
            Debug.Log("QuitButton");
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        });
    }
}
