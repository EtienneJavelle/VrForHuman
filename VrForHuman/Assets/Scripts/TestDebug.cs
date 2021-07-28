using CardiacMassage;
using RockVR.Video;
using UnityEngine;

public class TestDebug : MonoBehaviour {
    private VRInputModule vRInputModule;
    private ScoreManager scoreManager;

    public KeyCode AddScoreKey, RemoveScoreKey, EndGameKey,
        ClassicModeKey, ArcadeModeKey,
        StartRecordKey, PauseRecordKey, StopRecordKey;

    // Start is called before the first frame update
    private void Start() {
        vRInputModule = FindObjectOfType<VRInputModule>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    private void Update() {
#if UNITY_EDITOR

        if(vRInputModule != null && Input.GetKeyDown(ClassicModeKey)) {
            vRInputModule.ClassicMode();
        }

        if(vRInputModule != null && Input.GetKeyDown(ArcadeModeKey)) {
            vRInputModule.ArcadeMode();
        }

        if(Input.GetKeyDown(AddScoreKey) && scoreManager != null) {
            scoreManager.ChangeScore(1000);
        }

        if(Input.GetKeyDown(RemoveScoreKey) && scoreManager != null) {
            scoreManager.ChangeScore(1000);
        }

        if(Input.GetKeyDown(EndGameKey)) {
            GameManager.Instance.EndGame();
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
