using RockVR.Video;
using UnityEngine;

public class VideoMenu : MonoBehaviour {
    [SerializeField] private ButtonUI pauseButton, exitButton;

    public void ActiveButtonsObjects(bool _value) {
        pauseButton.gameObject.SetActive(_value);
        exitButton.gameObject.SetActive(_value);
    }

    private void PauseButton() {
        VideoPlayer.instance.SetPauseVideoPlayer();

        SetButtonPauseSprite();
    }

    public void SetButtonPauseSprite() {
        ButtonPause _buttonPause = pauseButton.GetComponent<ButtonPause>();
        _buttonPause.SetPauseSprite(VideoPlayer.instance.pauseActive);
    }

    private void ExitButton() {
        VideoPlayer.instance.SetCloseWindowVideoPlayer();
    }

    private void Start() {
        VideoPlayer.instance.SetVideoMenuButtons(this);
        ActiveButtonsObjects(false);

        pauseButton.onHandClick.AddListener(_ => PauseButton());

        exitButton.onHandClick.AddListener(_ => ExitButton());
    }
}
