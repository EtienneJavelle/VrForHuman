using UnityEngine;

public class PhoneCtrl : MonoBehaviour {
    #region UnityInspector

    [SerializeField] private PhoneInputField phoneInputField;

    [Space]

    [SerializeField] private ButtonUI callKey, specialCharLeftKey, specialCharRightKey, supprKey;

    [Space]

    [SerializeField] private ButtonUI[] keypad;

    #endregion

    private void Start() {
        /*for(int i = 0; i < keypad.Length; i++) {
            keypad[i].onHandClick.AddListener(_ => KeypadButton(keypad[i]));
        }

        specialCharLeftKey.onHandClick.AddListener(_ => KeypadButton(specialCharLeftKey));
        specialCharRightKey.onHandClick.AddListener(_ => KeypadButton(specialCharRightKey));*/

        supprKey.onHandClick.AddListener(_ => DeleteButton());

    }

    public void KeypadButton(ButtonUI _keypadButton) {
        ButtonCallKey _buttonCallKey = _keypadButton.GetComponent<ButtonCallKey>();

        phoneInputField.InputChar(_buttonCallKey.keyToDisplay);

        Debug.Log("KeypadButton");
    }

    private void DeleteButton() {

        phoneInputField.RemoveChar();

        Debug.Log("DeleteButton");
    }

}
