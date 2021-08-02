using CardiacMassage;
using RockVR.Video;
using UnityEngine;

public class TestDebug : MonoBehaviour {
    private MainMenu mainMenu;
    private ScoreManager scoreManager;

    public KeyCode AddScoreKey, RemoveScoreKey, EndGameKey,
        ClassicModeKey, ArcadeModeKey,
        StartRecordKey, PauseRecordKey, StopRecordKey;

    // Start is called before the first frame update
    private void Start() {
        mainMenu = FindObjectOfType<MainMenu>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    private void Update() {
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
        }
#endif
    }
}
