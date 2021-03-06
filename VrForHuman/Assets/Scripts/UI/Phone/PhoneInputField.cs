using TMPro;
using UnityEngine;

public class PhoneInputField : MonoBehaviour {
    #region UnityInspector

    [SerializeField] private TextMeshProUGUI inputField;
    [SerializeField] private int maxSizeText = 10;

    #endregion

    public string GetInputFieldText() {
        return inputField.text;
    }

    public void InputChar(string _text) {
        if(inputField.text.Length < maxSizeText) {
            inputField.text = inputField.text + _text;
        }
    }

    public void RemoveChar() {
        Debug.Log("Remove Char");
        if(inputField.text.Length >= 1) {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1, 1);
            Debug.Log(inputField.text);
        }
    }
}
