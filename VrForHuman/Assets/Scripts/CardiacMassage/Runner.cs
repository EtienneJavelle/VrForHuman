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
    private List<AudioSource> audioSources = new List<AudioSource>();
    #endregion

    private void Awake() {
        path ??= GetComponent<Etienne.Path>();
        runnerManager = GetComponentInParent<RunnerManager>();
    }

    private void Start() {
        audioSources.Add(AudioManager.Play(runningSound, transform));
        audioSources.Add(AudioManager.Play(breathingSound, transform));
        transform.DOLocalPath(path.LocalWaypoints, duration, PathType.CatmullRom, PathMode.Full3D).SetLookAt(0.01f).OnComplete(EndPath);
        if(runnerManager.isVictim) {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(duration - gruntSound.Clip.length).OnComplete(() => AudioManager.Play(gruntSound, transform));
        }
        anim.SetBool("IsRunning", true);

    }

    private void EndPath() {
        Debug.Log("End Path");
        foreach(AudioSource audioSource in audioSources) {
            audioSource.Stop();
        }
        if(runnerManager.isVictim) {
            AudioManager.Play(fallingSound, transform.position);
        }
        anim.SetBool("IsRunning", false);
        if(runnerManager.isVictim) {
            anim.SetBool("ArrestCardiacVictimView", true);
            runnerManager.ActiveCardiacMassage(true);
        } else {
            anim.SetBool("ArrestCardiacFriendView", true);
            DialogManager _dialogManager = runnerManager.GetComponent<DialogManager>();
            _dialogManager.LaunchDialog(0);
            runnerManager.friendDialogManager = _dialogManager;
            StartCoroutine(runnerManager.RescueAlertTimer());
        }
    }
}
