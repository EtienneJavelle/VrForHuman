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

    public void SetBoxDialogActive(bool _value) {
        canvas.gameObject.SetActive(_value);
    }

    public void LaunchDialog(int _currentDialogIndex) {
        currentTextIndex = 0;
        StartCoroutine(PlayDialogText(_currentDialogIndex));
    }

    private IEnumerator PlayDialogText(int _currentDialogIndex) {
        dialogLabel.text = dialogs[_currentDialogIndex].Texts[currentTextIndex];
        yield return new WaitForSeconds(textDuration);
        currentTextIndex++;
        if(currentTextIndex < dialogs[_currentDialogIndex].Texts.Length) {
            StartCoroutine(PlayDialogText(_currentDialogIndex));
        } else {
            dialogs[_currentDialogIndex].dialogCompleted.Invoke();
            SetBoxDialogActive(false);
        }
    }

}
