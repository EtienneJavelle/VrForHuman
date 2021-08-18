using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour {

    #region UnityInspector

    [SerializeField]
    private Etienne.Sound runningSound = new Etienne.Sound(null),
        gruntSound = new Etienne.Sound(null),
        fallingSound = new Etienne.Sound(null),
        breathingSound = new Etienne.Sound(null);
    [SerializeField] private Animator anim;
    [SerializeField] private Etienne.Path path;
    [SerializeField] private float duration = 5f;

    #endregion

    #region Fields
    private RunnerManager runnerManager;
    private RunnerFriend runnerFriend;
    private List<AudioSource> audioSources = new List<AudioSource>();
    #endregion

    private void Awake() {
        path ??= GetComponent<Etienne.Path>();
        runnerManager = GetComponentInParent<RunnerManager>();
        runnerFriend = GetComponentInParent<RunnerFriend>();
    }

    private void Start() {
        audioSources.Add(AudioManager.Play(runningSound, transform));
        audioSources.Add(AudioManager.Play(breathingSound, transform));
        transform.DOLocalPath(path.LocalWaypoints, duration, PathType.CatmullRom, PathMode.Full3D).SetLookAt(0.01f).OnComplete(EndBasePath);

        anim.SetTrigger("LaunchRun");
        anim.SetBool("IsRunning", true);

    }

    public void SetActiveVisual(bool _value) {
        anim.gameObject.SetActive(_value);
    }

    public void SetCurrentPath(Etienne.Path _path) {
        path = _path;
    }

    public void DefibrilatorPath() {
        anim.SetTrigger("LaunchRun");
        anim.SetBool("IsRunning", true);
        audioSources.Add(AudioManager.Play(runningSound, transform));
        audioSources.Add(AudioManager.Play(breathingSound, transform));
        transform.DOLocalPath(path.LocalWaypoints, duration, PathType.CatmullRom, PathMode.Full3D).SetLookAt(0.01f).OnComplete(EndDefibrilatorPath);
    }

    public void ReturnDefibrilatorPath() {
        anim.SetTrigger("LaunchRun");
        anim.SetBool("IsRunning", true);
        audioSources.Add(AudioManager.Play(runningSound, transform));
        audioSources.Add(AudioManager.Play(breathingSound, transform));
        transform.DOLocalPath(path.LocalWaypoints, duration, PathType.CatmullRom, PathMode.Full3D).SetLookAt(0.01f).OnComplete(EndReturnDefibrilatorPath);
    }

    private void EndBasePath() {
        Debug.Log("End Path");

        foreach(AudioSource audioSource in audioSources) {
            audioSource.Stop();
        }

        if(runnerManager.isVictim) {
            AudioManager.Play(fallingSound, transform.position);
            AudioManager.Play(gruntSound, transform);
        }
        anim.SetBool("IsRunning", false);
        if(runnerManager.isVictim) {
            anim.SetTrigger("ArrestCardiacVictimView");
            runnerManager.ActiveArrestCardiacSimulation(true);
        } else {
            anim.SetTrigger("ArrestCardiacFriendView");
            DialogManager _dialogManager = runnerManager.GetComponent<DialogManager>();
            _dialogManager.LaunchDialog(0);
            runnerManager.friendDialogManager = _dialogManager;
            StartCoroutine(runnerManager.RescueAlertTimer());
        }
    }

    private void EndDefibrilatorPath() {
        Debug.Log("End Defibrilator Path");

        foreach(AudioSource audioSource in audioSources) {
            audioSource.Stop();
        }

        SetActiveVisual(false);
        StartCoroutine(runnerFriend.TimerBeforeBringDefibrilator());
    }

    private void EndReturnDefibrilatorPath() {
        Debug.Log("End Return Defibrilator Path");

        foreach(AudioSource audioSource in audioSources) {
            audioSource.Stop();
        }

        anim.SetBool("IsRunning", false);
        anim.SetTrigger("ArrestCardiacFriendView");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -75f, transform.localEulerAngles.z);
        runnerManager.ActiveDefibrilator(true);
    }
}
