using UnityEngine;

public class PhoneCtrl : MonoBehaviour {
    #region UnityInspector

    [SerializeField] private PhoneInputField phoneInputField;
    [SerializeField] private GameObject baseScreenPhone, screenCall;

    [SerializeField] private int samuNumero;

    #endregion

    private void Awake() {

    }

    private void Start() {
        baseScreenPhone.SetActive(false);
        screenCall.SetActive(false);
    }

    public void PlayerPhoneCall(DialogManager _dialogManager, int _indexDialog) {

    }

    public void KeypadButton(ButtonUI _keypadButton) {
        ButtonCallKey _buttonCallKey = _keypadButton.GetComponent<ButtonCallKey>();

        phoneInputField.InputChar(_buttonCallKey.keyToDisplay);

        Debug.Log("KeypadButton");
    }

    public void DeleteButton() {

        phoneInputField.RemoveChar();

        Debug.Log("DeleteButton");
    }

    public void ActivePhoneScreen() {
        baseScreenPhone.SetActive(true);
    }

    public void CallButton() {
        Debug.Log("CallButton");
        if(phoneInputField.GetInputFieldText() == samuNumero.ToString()) {
            baseScreenPhone.SetActive(false);
            screenCall.SetActive(true);
        }
    }
}
