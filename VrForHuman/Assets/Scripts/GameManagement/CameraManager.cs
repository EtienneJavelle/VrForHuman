using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private GameObject replayVideoCanvas;

    private void Start() {
        SetActiveReplayVideoCanvas(false);
    }

    public void SetActiveReplayVideoCanvas(bool _value) {
        replayVideoCanvas.SetActive(_value);
    }
}
