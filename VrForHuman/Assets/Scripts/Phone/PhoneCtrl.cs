using DG.Tweening;
using Etienne;
using System.Collections;
using UnityEngine;

public class PhoneCtrl : InteractableSticker {

    #region Fields

    private AudioSource audioSource;
    private bool canHangup;
    private Coroutine endCoroutine;

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
    [SerializeField] private Sound hangupCallSound = new Sound(null);

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
        if(!screenCall.activeSelf) {
            baseScreenPhone.SetActive(true);
        }
        transform.localPosition = handPosition;
        transform.localEulerAngles = handEulerRotation;
        hand.useHoverSphere = false;
        inputZone.enabled = true;
    }

    private void RemovePhoneFromHand() {
        baseScreenPhone.SetActive(false);
        hand.useHoverSphere = true;
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
        runnerManager.DialogManager.SetBoxDialogActive(false);
    }

    private void PhoneEndPath() {
        Debug.Log("PhoneEndPath");
        transform.localEulerAngles = new Vector3(0, 180, 0);
    }

    public void EndCallRescue() {
        endCoroutine = StartCoroutine(EndCallRescueCoroutine());
    }

    private IEnumerator EndCallRescueCoroutine() {

        canHangup = true;
        yield return WaitSoundCompleted(phoneDialogManager.GetDialog(3).DialogSounds[0]);

        phoneDialogManager.SetBoxDialogActive(false);

        yield return PhoneSound(quitCallSound);
        AudioManager.Play(hangupCallSound, transform);

        screenCall.SetActive(false);

        Debug.Log("Le joueur peut commencer le massage cardiaque");
        runnerManager.ActiveCardiacMassage(true);
        GameManager.Instance.CardiacMassage.ActiveCardiacMassageDialog();
    }

    public void Hangup() {
        if(canHangup && endCoroutine != null) {
            StopCoroutine(endCoroutine);
            if(audioSource != null & audioSource.isPlaying) {
                audioSource.Stop();
            }
            TestDebug.Instance.EndRescueStep03();
            AudioManager.Play(hangupCallSound, transform);
            phoneDialogManager.SetBoxDialogActive(false);
            screenCall.SetActive(false);
            runnerManager.ActiveCardiacMassage(true);
        }
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
