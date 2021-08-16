using DG.Tweening;
using UnityEngine;

public class Runner : MonoBehaviour {

    #region UnityInspector

    [SerializeField] private Animator anim;
    [SerializeField] private Etienne.Path path;
    [SerializeField] private float duration = 5f;

    #endregion

    #region Fields
    private RunnerManager runnerManager;
    #endregion

    private void Awake() {
        path ??= GetComponent<Etienne.Path>();
        runnerManager = GetComponentInParent<RunnerManager>();
    }

    private void Start() {
        transform.DOLocalPath(path.LocalWaypoints, duration, PathType.CatmullRom, PathMode.Full3D, 10, Color.blue).SetLookAt(0.01f).OnComplete(EndPath);
        anim.SetBool("IsRunning", true);

    }

    private void EndPath() {
        Debug.Log("End Path");
        anim.SetBool("IsRunning", false);
        if(runnerManager.isVictim) {
            anim.SetBool("ArrestCardiacVictimView", true);
            runnerManager.ActiveCardiacMassage(true);
        } else {
            anim.SetBool("ArrestCardiacFriendView", true);
            DialogManager _dialogManager = runnerManager.GetComponent<DialogManager>();
            _dialogManager.LaunchDialog(0);
            TestDebug.Instance.friendDialogManager = _dialogManager;
        }
    }
}
