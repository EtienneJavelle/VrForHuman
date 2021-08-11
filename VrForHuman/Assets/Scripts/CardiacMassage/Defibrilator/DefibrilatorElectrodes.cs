using DG.Tweening;
using UnityEngine;

public class DefibrilatorElectrodes : InteractableSticker {
    public event System.Action OnPlaced;

    public bool IsPlaced => isPlaced;

    private bool isPlaced;

    private void OnTriggerEnter(Collider other) {
        Detach(true);
        transform.DOMove(other.transform.position, .1f);
        transform.DORotateQuaternion(other.transform.rotation, .1f).OnComplete(Place);
    }

    private void Place() {
        isPlaced = true;
        OnPlaced?.Invoke();
    }
}
