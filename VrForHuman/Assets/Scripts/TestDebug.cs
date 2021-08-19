using CardiacMassage;
using RockVR.Video;
using RockVR.Video.Demo;
using UnityEditor;
using UnityEngine;

public class TestDebug : Etienne.Singleton<TestDebug> {
    private MainMenu mainMenu;
    private ScoreManager scoreManager;

    private VideoCaptureUI videoCaptureUI;
    private RecordManager recordManager;

    private PhoneCtrl phoneCtrl;
    private AudioSource lastDialogueAudioSource;

    private bool callRescueStep01Completed, callRescueStep02Completed, callRescueStep03Completed;

    public KeyCode AddScoreKey, RemoveScoreKey, EndGameKey,
        ClassicModeKey, ArcadeModeKey,
        StartRecordKey, PauseRecordKey, StopRecordKey, PlayVideoKey, NextVideoKey, PreviousVideoKey, ExitVideoModeKey,
        ExitGameKey;

    [Space]

    [SerializeField] private KeyCode callRescueStep01Key;
    [SerializeField] private KeyCode callRescueStep02Key;
    [SerializeField] private KeyCode callRescueStep03Key;

    [SerializeField] private KeyCode activePhoneKey, launchRescueCallKey;

    // Start is called before the first frame update
    private void Start() {
        mainMenu = FindObjectOfType<MainMenu>();
        scoreManager = FindObjectOfType<ScoreManager>();

        videoCaptureUI = FindObjectOfType<VideoCaptureUI>();
        recordManager = FindObjectOfType<RecordManager>();
    }

    public void SetPhoneCtrl(PhoneCtrl _phoneCtrl) {
        phoneCtrl = _phoneCtrl;
    }

    private void GameModeDebug() {
        if(mainMenu != null && Input.GetKeyDown(ClassicModeKey)) {
            mainMenu.ClassicMode();
        }

        if(mainMenu != null && Input.GetKeyDown(ArcadeModeKey)) {
            mainMenu.ArcadeMode();
        }

        if(Input.GetKeyDown(EndGameKey)) {
            GameManager.Instance.EndSimulation();
        }
    }

    private void ScoreDebug() {
        if(Input.GetKeyDown(AddScoreKey) && scoreManager != null) {
            scoreManager.ChangeScore(1000);
        }

        if(Input.GetKeyDown(RemoveScoreKey) && scoreManager != null) {
            scoreManager.ChangeScore(1000);
        }
    }

    private void RecordDebug() {
        if(VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.NOT_START && Input.GetKeyDown(StartRecordKey)) {
            VideoCaptureCtrl.Instance.StartCapture();
            UnityEngine.Debug.Log("START");
        } else if(VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.STARTED) {
            if(Input.GetKeyDown(StopRecordKey)) {
                VideoCaptureCtrl.Instance.StopCapture();
                UnityEngine.Debug.Log("STOP");
            }
            if(Input.GetKeyDown(PauseRecordKey)) {
                VideoCaptureCtrl.Instance.ToggleCapture();
                UnityEngine.Debug.Log("PAUSE");
            }
        } else if(VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.FINISH) {
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
    }

    private void CallRescueStepsDebug() {

        if(Input.GetKeyDown(callRescueStep01Key)) {
            CallRescueStep01Completed();
        }

        if(Input.GetKeyDown(callRescueStep02Key)) {
            CallRescueStep02Completed();
        }

        if(Input.GetKeyDown(callRescueStep03Key)) {
            CallRescueStep03Completed();
        }
    }

    public void CallRescueStep01Completed() {
        if(phoneCtrl.phoneDialogManager.GetDialog(0).dialogCompleted && callRescueStep01Completed == false) {
            Debug.Log("Call Rescue Step 01 Completed");
            phoneCtrl.phoneDialogManager.LaunchDialog(1);
            callRescueStep01Completed = true;
        }
    }

    public void CallRescueStep02Completed() {
        if(phoneCtrl.phoneDialogManager.GetDialog(1).dialogCompleted && callRescueStep02Completed == false) {
            Debug.Log("Call Rescue Step 02 Completed");
            phoneCtrl.phoneDialogManager.LaunchDialog(2);
            callRescueStep02Completed = true;
        }
    }

    public void CallRescueStep03Completed() {
        if(phoneCtrl.phoneDialogManager.GetDialog(2).dialogCompleted && callRescueStep03Completed == false) {
            Debug.Log("Call Rescue Step 03 Completed");
            lastDialogueAudioSource = phoneCtrl.phoneDialogManager.LaunchDialog(3);
            GameManager.Instance.PlayerCanvasManager.ActiveCityDisplay(false);
            GameManager.Instance.PlayerCanvasManager.ActivePhoneNumberDisplay(false);
            callRescueStep03Completed = true;
        }
    }

    public void EndRescueStep03() {
        lastDialogueAudioSource.Stop();
    }

    // Update is called once per frame
    private void Update() {

        if(Input.GetKeyDown(activePhoneKey)) {
            phoneCtrl.ActivePhoneScreen();
        }

        if(Input.GetKeyDown(launchRescueCallKey)) {
            StartCoroutine(phoneCtrl.StartRecueCall());
        }

        if(Input.GetKeyDown(ExitGameKey)) {
            Application.Quit();

#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
        }

#if UNITY_EDITOR

        GameModeDebug();

        ScoreDebug();

        RecordDebug();

        CallRescueStepsDebug();
#endif
    }
}
