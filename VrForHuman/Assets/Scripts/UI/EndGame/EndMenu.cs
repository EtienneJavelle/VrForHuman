#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using RockVR.Video;
using System.IO;

public class EndMenu : MonoBehaviour {
    [SerializeField] private ButtonUI menuButton, quitButton;
    [SerializeField] private ButtonUI saveVideosButton, notSaveVideosButton;

    [SerializeField] private GameObject saveVideosObject;
    private bool goToMenu, goToExit;

    private void Start() {

        saveVideosObject.SetActive(false);

        menuButton.onHandClick.AddListener(_ => MenuButton());

        quitButton.onHandClick.AddListener(_ => QuitButton());

        saveVideosButton.onHandClick.AddListener(_ => SavedVideoButton());

        notSaveVideosButton.onHandClick.AddListener(_ => NotSavedVideoButton());
    }

    private void MenuButton() {
        goToMenu = true;
        saveVideosObject.SetActive(true);

        Debug.Log("MenuButton");
    }

    private void QuitButton() {
        Debug.Log("QuitButton");

        goToExit = true;
        saveVideosObject.SetActive(true);
    }

    private void SavedVideoButton() {
        Debug.Log("SaveVideosButton");

        if(VideoPlayer.instance != null) {
            VideoPlayer.instance.saving = true;
        }

        if(VideoCaptureCtrl.Instance != null) {
            VideoCaptureCtrl.Instance.saving = true;
        }

        ExitGame();

        MainMenu();
    }

    private void NotSavedVideoButton() {
        Debug.Log("NotSaveVideosButton");

        if(VideoPlayer.instance != null) {
            for(int i = 0; i < VideoPlayer.instance.currentVideoFiles.Count; i++) {
                File.Delete(VideoPlayer.instance.currentVideoFiles[i]);
            }
        }

        ExitGame();

        MainMenu();
    }

    public void ExitGame() {
        if(goToExit) {
            VideoPlayer.instance.SetCloseWindowVideoPlayer();
            Application.Quit();
            goToExit = false;
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }
    }

    public void MainMenu() {
        if(goToMenu) {
            VideoPlayer.instance.SetCloseWindowVideoPlayer();
            VideoCaptureCtrl.Instance.InitStatutCapture();
            VideoPlayer.instance.SetParentVideoPlayerImpl(VideoPlayer.instance.transform);
            SceneLoader.Instance.ChangeScene(Scenes.MainMenu);
            goToMenu = false;
        }
    }
}
