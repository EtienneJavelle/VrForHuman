using DG.Tweening;
using UnityEngine;

public class PhoneCtrl : MonoBehaviour {
    #region UnityInspector

    [SerializeField] private PhoneInputField phoneInputField;
    [SerializeField] private GameObject baseScreenPhone, screenCall;

    [SerializeField] private int samuNumero;

    [SerializeField] private Etienne.Path path;
    [SerializeField] private float duration = 5f;

    #endregion

    private void Awake() {
        path ??= GetComponent<Etienne.Path>();
    }

    private void Start() {
        baseScreenPhone.SetActive(false);
        screenCall.SetActive(false);
    }

    public void PlayerCanUsePhone() {
        Debug.Log("Can Use Phone");
        transform.parent = null;
        transform.DOLocalPath(path.LocalWaypoints, duration, PathType.CatmullRom, PathMode.Full3D, 10, Color.blue).SetLookAt(0.01f).OnComplete(PhoneEndPath);
    }

    private void PhoneEndPath() {
        Debug.Log("PhoneEndPath");
        baseScreenPhone.SetActive(true);
    }

    public void PlayerPhoneCallRequest(DialogManager _dialogManager) {
        _dialogManager.LaunchDialog(2);
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
