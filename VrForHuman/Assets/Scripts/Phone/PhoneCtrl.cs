using DG.Tweening;
using Etienne;
using System.Collections;
using UnityEngine;

public class PhoneCtrl : InteractableSticker {

    #region Fields

    private AudioSource audioSource;

    #endregion

    #region Properties

    public DialogManager phoneDialogManager { get; protected set; }

    #endregion

    #region UnityInspector
    [SerializeField] private Vector3 handPosition, handEulerRotation;
    [SerializeField] private BoxCollider phoneCollider, inputZone;


    [Header("Phone")]
    [SerializeField] private PhoneInputField phoneInputField;
    [SerializeField] private GameObject baseScreenPhone, screenCall;

    [SerializeField] private RunnerManager runnerManager;
    [SerializeField] private RunnerFriend runnerFriend;

    [SerializeField] private Sound launchCallSound = new Sound(null);
    [SerializeField] private Sound quitCallSound = new Sound(null);

    [SerializeField] private int samuNumero;
    [SerializeField] private int emergencyNumero;

    [SerializeField] private Path path;
    [SerializeField] private float duration = 5f;

    #endregion

    private void Awake() {
        path ??= GetComponent<Path>();
        phoneDialogManager = GetComponent<DialogManager>();
        TestDebug.Instance.SetPhoneCtrl(this);
    }

    protected override void Start() {
        base.Start();
        baseScreenPhone.SetActive(false);
        screenCall.SetActive(false);
        OnAttach += PlacePhoneInHand;
        OnDetach += RemovePhoneFromHand;
        rb.detectCollisions = false;
        phoneCollider.isTrigger = false;
        phoneCollider.enabled = true;
        inputZone.isTrigger = true;
        inputZone.enabled = false;
    }

    public IEnumerator StartRecueCall() {
        baseScreenPhone.SetActive(false);
        screenCall.SetActive(true);

        yield return PhoneSound(launchCallSound);
        //AudioManager.Play(launchCallSound, transform);

        phoneDialogManager.LaunchDialog(0);
    }

    private void PlacePhoneInHand() {
        Debug.Log("In Hand");
        baseScreenPhone.SetActive(true);
        transform.localPosition = handPosition;
        transform.localEulerAngles = handEulerRotation;
        hand.useHoverSphere = false;
        //phoneCollider.enabled = false;
        inputZone.enabled = true;
    }

    private void RemovePhoneFromHand() {
        Debug.Log("off Hand");
        baseScreenPhone.SetActive(false);
        hand.useHoverSphere = true;
        //phoneCollider.enabled = true;
        inputZone.enabled = false;
    }

    public void DialogCompleted(int _dialogIndex) {
        phoneDialogManager.GetDialog(_dialogIndex).dialogCompleted = true;
        Debug.Log("Dialog Completed " + _dialogIndex);
    }

    public void PlayerCanUsePhone() {
        Debug.Log("Can Use Phone");
        rb.detectCollisions = true;
        transform.parent = null;
        //TODO: Faire en sorte que le téléphone colle à la main
        transform.DOLocalPath(path.LocalWaypoints, duration, PathType.CatmullRom, PathMode.Full3D, 10, Color.blue).SetLookAt(0.01f).OnComplete(PhoneEndPath);


        GameManager.Instance.PlayerCanvasManager.GetCityDisplay().fading = false;
        GameManager.Instance.PlayerCanvasManager.ActiveCityDisplay(true);
        GameManager.Instance.PlayerCanvasManager.GetCityDisplay().SetDisplayTextColorVisible();

        GameManager.Instance.PlayerCanvasManager.ActivePhoneNumberDisplay(true);

        Debug.Log("L'ami part chercher le défibrilateur");
        runnerFriend.SetCurrentPathToDefibrilatorPath();
        runnerManager.friendDialogManager.SetBoxDialogActive(false);
    }

    private void PhoneEndPath() {
        Debug.Log("PhoneEndPath");
        transform.localEulerAngles = new Vector3(0, 180, 0);
    }

    public void EndCallRescue() {
        StartCoroutine(EndCallRescueCoroutine());
    }

    private IEnumerator EndCallRescueCoroutine() {

        yield return WaitSoundCompleted(phoneDialogManager.GetDialog(3).DialogSound);

        phoneDialogManager.SetBoxDialogActive(false);

        yield return PhoneSound(quitCallSound);

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
        AudioManager.Play(_buttonCallKey.touchPhoneSound);

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
        StartCoroutine(CallButtonCoroutine());
    }

    private IEnumerator CallButtonCoroutine() {
        if(phoneInputField.GetInputFieldText() == samuNumero.ToString() ||
            phoneInputField.GetInputFieldText() == emergencyNumero.ToString()) {
            baseScreenPhone.SetActive(false);
            screenCall.SetActive(true);

            yield return PhoneSound(launchCallSound);
            //AudioManager.Play(launchCallSound, transform);

            phoneDialogManager.LaunchDialog(0);
        }
    }

    private WaitForSeconds PhoneSound(Sound sound) {
        audioSource = AudioManager.Play(sound, transform);
        return new WaitForSeconds(sound.Clip.length);
    }

    private WaitForSeconds WaitSoundCompleted(Sound sound) {
        return new WaitForSeconds(sound.Clip.length);
    }
}
