using System.Collections;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour {
    #region UnityInspector

    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI dialogLabel;

    [SerializeField] private Dialog[] dialogs;

    [SerializeField] private float textDuration = 1.5f;

    private int currentTextIndex;

    #endregion

    private AudioSource audioSource;

    private void Start() {
        SetBoxDialogActive(false);
    }

    public Dialog GetDialog(int _index) {
        return dialogs[_index];
    }

    public void SetBoxDialogActive(bool _value) {
        canvas.gameObject.SetActive(_value);
    }

    public AudioSource LaunchDialog(int _currentDialogIndex) {
        SetBoxDialogActive(true);
        currentTextIndex = 0;
        StartCoroutine(PlayDialogText(_currentDialogIndex));
        return audioSource;
    }

    private IEnumerator PlayDialogText(int _currentDialogIndex) {
        dialogLabel.text = dialogs[_currentDialogIndex].Texts[currentTextIndex];

        if(dialogs[_currentDialogIndex].DialogSounds.Length >= 1) {
            audioSource = AudioManager.Play(dialogs[_currentDialogIndex].DialogSounds[currentTextIndex], transform.GetChild(0));
        }

        yield return new WaitForSeconds(textDuration);
        currentTextIndex++;
        if(currentTextIndex < dialogs[_currentDialogIndex].Texts.Length) {
            StartCoroutine(PlayDialogText(_currentDialogIndex));
        } else {
            //SetBoxDialogActive(false);
            dialogs[_currentDialogIndex].OnCompleted.Invoke();
        }
    }

}
