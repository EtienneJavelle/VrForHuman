using DG.Tweening;
using UnityEngine;

public class DefibrilatorTargetEmplacement : MonoBehaviour {
    [ContextMenu("Enable")]
    public void Enable() {
        transform.DOPunchScale(transform.localScale * 1.5f, .2f);
        GetComponent<MeshRenderer>().material.DOFade(1, .1f).SetLoops(2, LoopType.Yoyo);
    }
}
