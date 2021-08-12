using UnityEngine;

public class PhoneCtrl : MonoBehaviour {
    #region UnityInspector

    [SerializeField] private PhoneInputField phoneInputField;
    [SerializeField] private GameObject baseScreenPhone, screenCall;

    [SerializeField] private int samuNumero;

    //[Space]

    //[SerializeField] private ButtonUI callKey, /*specialCharLeftKey, specialCharRightKey,*/ supprKey;

    //[Space]

    //[SerializeField] private ButtonUI[] keypad;

    #endregion

    private void Start() {
        /*for(int i = 0; i < keypad.Length; i++) {
            keypad[i].onHandClick.AddListener(_ => KeypadButton(keypad[i]));
        }

        specialCharLeftKey.onHandClick.AddListener(_ => KeypadButton(specialCharLeftKey));
        specialCharRightKey.onHandClick.AddListener(_ => KeypadButton(specialCharRightKey));*/

        //supprKey.onHandClick.AddListener(_ => DeleteButton());

        baseScreenPhone.SetActive(true);
        screenCall.SetActive(false);

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

    public void CallButton() {
        Debug.Log("CallButton");
        if(phoneInputField.GetInputFieldText() == samuNumero.ToString()) {
            baseScreenPhone.SetActive(false);
            screenCall.SetActive(true);
        }
    }
}
