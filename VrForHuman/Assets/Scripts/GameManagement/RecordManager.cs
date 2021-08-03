using RockVR.Video;
using RockVR.Video.Demo;
using UnityEngine;

public class RecordManager : MonoBehaviour {

    private VideoCaptureUI videoCaptureUI;

    private void Awake() {
        GameManager.Instance.SetRecordManager(this);

        videoCaptureUI = FindObjectOfType<VideoCaptureUI>();
    }

    // Start is called before the first frame update
    private void Start() {
        InitRecord();

        StartRecord();
    }

    private void InitRecord() {
        VideoCapture[] _camerasViews = FindObjectsOfType<VideoCapture>();
        VideoCaptureCtrl.instance.videoCaptures = _camerasViews;
    }

    public void StartRecord() {
        if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.NOT_START) {
            VideoCaptureCtrl.instance.StartCapture();
            UnityEngine.Debug.Log("START");
        }
    }

    public void PauseRecord() {
        if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED) {
            VideoCaptureCtrl.instance.ToggleCapture();
            UnityEngine.Debug.Log("PAUSE");
        }
    }

    public void StopRecord() {
        if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED) {
            VideoCaptureCtrl.instance.StopCapture();
            UnityEngine.Debug.Log("STOP");
        }
    }

    public void PlayVideo() {
        if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH) {
            if(!videoCaptureUI.isPlayVideo) {
#if UNITY_5_6_OR_NEWER
                // Set root folder.
                videoCaptureUI.isPlayVideo = true;
                VideoPlayer.instance.SetRootFolder();
                // Play capture video.
                VideoPlayer.instance.PlayVideo();
                UnityEngine.Debug.Log("PLAY VIDEO");
            }
        }
    }
#endif

    public void NextVideo() {
        if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH) {
            if(videoCaptureUI.isPlayVideo) {
                // Turn to next video.
                VideoPlayer.instance.NextVideo();
                UnityEngine.Debug.Log("NEXT VIDEO");
                // Play capture video.
                VideoPlayer.instance.PlayVideo();
#if !UNITY_5_6_OR_NEWER
            // Open video save directory.
            Process.Start(PathConfig.saveFolder);
#endif
            }
        }

    }
}
