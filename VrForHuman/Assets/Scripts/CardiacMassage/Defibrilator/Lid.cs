using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Lid : MonoBehaviour {
    [SerializeField] private Vector3 openedRotation, closedRotation;

    [ContextMenu("Open")]
    public TweenerCore<Quaternion, Vector3, QuaternionOptions> Open() {
        Debug.Log("Open");
        return transform.DORotate(openedRotation, .5f);
    }
    [ContextMenu("Close")]
    public TweenerCore<Quaternion, Vector3, QuaternionOptions> Close() {
        Debug.Log("Close");
        return transform.DORotate(closedRotation, .5f);
    }
}
