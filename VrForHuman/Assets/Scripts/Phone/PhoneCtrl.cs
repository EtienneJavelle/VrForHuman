using DG.Tweening;
using UnityEngine;

public class PhoneCtrl : MonoBehaviour {
    #region Properties

    public DialogManager phoneDialogManager { get; protected set; }

    #endregion

    #region UnityInspector

    [SerializeField] private PhoneInputField phoneInputField;
    [SerializeField] private GameObject baseScreenPhone, screenCall;

    [SerializeField] private RunnerManager runnerManager;
    [SerializeField] private RunnerFriend runnerFriend;

    [SerializeField] private Etienne.Sound callSound = new Etienne.Sound(null);

    [SerializeField] private int samuNumero;

    [SerializeField] private Etienne.Path path;
    [SerializeField] private float duration = 5f;

    #endregion

    private void Awake() {
        path ??= GetComponent<Etienne.Path>();
        phoneDialogManager = GetComponent<DialogManager>();
        TestDebug.Instance.SetPhoneCtrl(this);
    }

    private void Start() {
        baseScreenPhone.SetActive(false);
        screenCall.SetActive(false);
    }

    public void DialogCompleted(int _dialogIndex) {
        phoneDialogManager.GetDialog(_dialogIndex).dialogCompleted = true;
        Debug.Log("Dialog Completed " + _dialogIndex);
    }

    public void PlayerCanUsePhone() {
        Debug.Log("Can Use Phone");
        transform.parent = null;
        //TODO: Faire en sorte que le téléphone colle à la main
        transform.DOLocalPath(path.LocalWaypoints, duration, PathType.CatmullRom, PathMode.Full3D, 10, Color.blue).SetLookAt(0.01f).OnComplete(PhoneEndPath);


        GameManager.Instance.PlayerCanvasManager.GetCityDisplay().fading = false;
        GameManager.Instance.PlayerCanvasManager.ActiveCityDisplay(true);
        GameManager.Instance.PlayerCanvasManager.GetCityDisplay().SetDisplayTextColorVisible();

        GameManager.Instance.PlayerCanvasManager.ActivePhoneNumberDisplay(true);

        Debug.Log("L'ami part chercher le défibrilateur");
        runnerFriend.SetCurrentPathToDefibrilatorPath();
    }

    private void PhoneEndPath() {
        Debug.Log("PhoneEndPath");
        baseScreenPhone.SetActive(true);
        transform.localEulerAngles = new Vector3(0, 180, 0);
    }

    public void EndCallRescue() {
        screenCall.SetActive(false);

        Debug.Log("Le joueur peut commencer le massage cardiaque");
        runnerManager.ActiveCardiacMassage(true);
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

            //AudioManager.Play(callSound, transform);

            phoneDialogManager.LaunchDialog(0);
        }
    }
}
