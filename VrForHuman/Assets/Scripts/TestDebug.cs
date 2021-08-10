using CardiacMassage;
using RockVR.Video;
using RockVR.Video.Demo;
using UnityEditor;
using UnityEngine;

public class TestDebug : MonoBehaviour {
    private MainMenu mainMenu;
    private ScoreManager scoreManager;

    private VideoCaptureUI videoCaptureUI;
    private RecordManager recordManager;

    public KeyCode AddScoreKey, RemoveScoreKey, EndGameKey,
        ClassicModeKey, ArcadeModeKey,
        StartRecordKey, PauseRecordKey, StopRecordKey, PlayVideoKey, NextVideoKey, PreviousVideoKey, ExitVideoModeKey,
        ExitGameKey;

    // Start is called before the first frame update
    private void Start() {
        mainMenu = FindObjectOfType<MainMenu>();
        scoreManager = FindObjectOfType<ScoreManager>();

        videoCaptureUI = FindObjectOfType<VideoCaptureUI>();
        recordManager = FindObjectOfType<RecordManager>();
    }

    // Update is called once per frame
    private void Update() {

        if(Input.GetKeyDown(ExitGameKey)) {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }

#if UNITY_EDITOR

        if(mainMenu != null && Input.GetKeyDown(ClassicModeKey)) {
            mainMenu.ClassicMode();
        }

        if(mainMenu != null && Input.GetKeyDown(ArcadeModeKey)) {
            mainMenu.ArcadeMode();
        }

        if(Input.GetKeyDown(AddScoreKey) && scoreManager != null) {
            scoreManager.ChangeScore(1000);
        }

        if(Input.GetKeyDown(RemoveScoreKey) && scoreManager != null) {
            scoreManager.ChangeScore(1000);
        }

        if(Input.GetKeyDown(EndGameKey)) {
            GameManager.Instance.EndSimulation();
        }

        if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.NOT_START && Input.GetKeyDown(StartRecordKey)) {
            VideoCaptureCtrl.instance.StartCapture();
            UnityEngine.Debug.Log("START");
        } else if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED) {
            if(Input.GetKeyDown(StopRecordKey)) {
                VideoCaptureCtrl.instance.StopCapture();
                UnityEngine.Debug.Log("STOP");
            }
            if(Input.GetKeyDown(PauseRecordKey)) {
                VideoCaptureCtrl.instance.ToggleCapture();
                UnityEngine.Debug.Log("PAUSE");
            }
        } else if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH) {
            if(!videoCaptureUI.isPlayVideo) {
                if(Input.GetKeyDown(PlayVideoKey)) {
#if UNITY_5_6_OR_NEWER
                    // Set root folder.
                    videoCaptureUI.isPlayVideo = true;
                    VideoPlayer.instance.SetRootFolder();
                    // Play capture video.
                    VideoPlayer.instance.PlayVideo();
                    UnityEngine.Debug.Log("PLAY VIDEO");
                }
            } else {
                if(Input.GetKeyDown(NextVideoKey)) {
                    // Turn to next video.
                    VideoPlayer.instance.NextVideo();
                    UnityEngine.Debug.Log("NEXT VIDEO");
                    // Play capture video.
                    VideoPlayer.instance.PlayVideo();
#else
                        // Open video save directory.
                        Process.Start(PathConfig.saveFolder);
#endif
                }
                if(Input.GetKeyDown(PreviousVideoKey)) {
                    //Turn to previous video.
                    VideoPlayer.instance.PreviousVideo();
                    UnityEngine.Debug.Log("PREVIOUS VIDEO");
                }
                /*if(Input.GetKeyDown(ExitVideoModeKey)) {
                    VideoPlayer.instance.ExitVideoMode();
                    videoCaptureUI.isPlayVideo = false;
                }*/
            }
        }
#endif
    }
}
