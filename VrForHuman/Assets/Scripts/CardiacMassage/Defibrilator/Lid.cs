using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(InteractableHoverEvents))]
public class Lid : MonoBehaviour {
    public event Action OnOpenLid, OnCloseLid;

    [SerializeField] private Vector3 openedRotation, closedRotation;

    private bool isOpened, isMoving;

    private void Awake() {
        GetComponent<InteractableHoverEvents>().onHandHoverBegin.AddListener(Toggle);
    }

    public void Toggle() {
        Debug.Log("Toggle");
        if(isMoving) {
            return;
        }
        if(isOpened) {
            Close();
        } else {
            Open();
        }
    }

    [ContextMenu("Open")]
    public TweenerCore<Quaternion, Quaternion, NoOptions> Open() {
        Debug.Log("Open");
        OnOpenLid?.Invoke();
        return transform.DOLocalRotateQuaternion(Quaternion.Euler(openedRotation), .5f).OnComplete(Opened);
    }
    [ContextMenu("Close")]
    public TweenerCore<Quaternion, Quaternion, NoOptions> Close() {
        Debug.Log("Close");
        OnCloseLid?.Invoke();
        return transform.DOLocalRotateQuaternion(Quaternion.Euler(closedRotation), .5f).OnComplete(Closed);
    }

    private void Opened() {
        Completed(true);
    }

    private void Closed() {
        Completed(false);
    }

    private void Completed(bool isOpened) {
        isMoving = false;
        this.isOpened = isOpened;
    }
}
