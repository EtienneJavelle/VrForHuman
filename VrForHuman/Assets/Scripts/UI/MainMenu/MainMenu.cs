using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] private ButtonUI classicModeButton, arcadeModeButton, quitButton;

    private void Start() {
        classicModeButton.onHandClick.AddListener(_ => {
            ClassicMode();
            Debug.Log("ClassicModeButton");
        }
        );

        arcadeModeButton.onHandClick.AddListener(_ => {
            ArcadeMode();
            Debug.Log("ArcadeModeButton");
        }
        );

        quitButton.onHandClick.AddListener(_ => {
            Debug.Log("QuitButton");
            QuitGame();
        });
    }

    public void ClassicMode() {
        GameManager.Instance.IsArcadeMode = false;
        if(GameManager.Instance.PlayerCanvasManager != null) {
            GameManager.Instance.PlayerCanvasManager.ActiveCityDisplay(true);
        }

        SceneLoader.Instance.ChangeScene(Scenes.CardiacMassage);
    }

    [ContextMenu("ArcadeMode")]
    public void ArcadeMode() {
        GameManager.Instance.IsArcadeMode = true;
        if(GameManager.Instance.PlayerCanvasManager != null) {
            GameManager.Instance.PlayerCanvasManager.ActiveCityDisplay(true);
        }

        SceneLoader.Instance.ChangeScene(Scenes.CardiacMassage);
    }

    public void QuitGame() {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}
