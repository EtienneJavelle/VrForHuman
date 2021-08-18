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

    private void Start() {
        SetBoxDialogActive(false);
    }

    public Dialog GetDialog(int _index) {
        return dialogs[_index];
    }

    public void SetBoxDialogActive(bool _value) {
        canvas.gameObject.SetActive(_value);
    }

    public void LaunchDialog(int _currentDialogIndex) {
        SetBoxDialogActive(true);
        currentTextIndex = 0;
        StartCoroutine(PlayDialogText(_currentDialogIndex));
    }

    private IEnumerator PlayDialogText(int _currentDialogIndex) {
        dialogLabel.text = dialogs[_currentDialogIndex].Texts[currentTextIndex];

        AudioManager.Play(dialogs[_currentDialogIndex].DialogSound, transform);

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
