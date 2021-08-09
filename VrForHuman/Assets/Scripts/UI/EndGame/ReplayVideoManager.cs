using RockVR.Video;
using RockVR.Video.Demo;
using UnityEngine;

public class ReplayVideoManager : MonoBehaviour {
    #region Fields

    private VideoCaptureUI videoCaptureUI;

    //private List<ReplayVideoButton> listReplayVideosButtons = new List<ReplayVideoButton>();

    #endregion

    #region UnityInspector

    [SerializeField] private ReplayVideoButton videoButtonPrefab;

    #endregion

    private void Awake() {
        videoCaptureUI = FindObjectOfType<VideoCaptureUI>();
    }

    private void Start() {

        VideoPlayer.instance.SetParentVideoPlayerImpl(transform.parent);
        VideoPlayer.instance.SetTransformVideoPlayerImpl(transform);
        VideoPlayer.instance.SetBaseSizeVideoPlayerImpl();

        for(int i = 0; i < VideoPlayer.instance.currentVideoFiles.Count; i++) {
            ReplayVideoButton videoButton = Instantiate(videoButtonPrefab);
            videoButton.transform.SetParent(transform);
            videoButton.transform.GetComponent<RectTransform>().localRotation = Quaternion.identity;
            videoButton.transform.localPosition = Vector3.zero;
            videoButton.transform.localScale = Vector3.one;
            videoButton.videoIndex = i;
            videoButton.GetComponent<UnityEngine.Video.VideoPlayer>().url =
                VideoPlayer.instance.currentVideoFiles[i];
            StartCoroutine(videoButton.VideoPlayerTimerActivation());
            //listReplayVideosButtons.Add(videoButton);

            videoButton.onHandClick.AddListener(_ => {
                if(GameManager.Instance.replayVideoIsPlaying == false) {
                    PlayVideoAtIndex(videoButton.videoIndex);
                    Debug.Log("VideoButton");
                }
            }
        );
        }
    }

    public void PlayVideo() {
        if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH) {
#if UNITY_5_6_OR_NEWER
            // Play capture video.
            VideoPlayer.instance.PlayVideo();
            UnityEngine.Debug.Log("PLAY VIDEO");
        }
    }
#endif

    public void PlayVideoAtIndex(int _index) {
        if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH) {
#if UNITY_5_6_OR_NEWER
            // Play capture video.
            VideoPlayer.instance.PlayVideoAtIndex(_index);

            VideoPlayer.instance.videoPlayerActive = true;
            //Camera.main.GetComponent<CameraManager>().SetActiveReplayVideoCanvas(true);
            //VideoPlayer.instance.SetVideoPlayerImplCameraTarget(Camera.main);

            GameManager.Instance.replayVideoIsPlaying = true;
            UnityEngine.Debug.Log("PLAY VIDEO " + _index);
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
