using DG.Tweening;
using UnityEngine;

public class DefibrilatorElectrodes : InteractableSticker {
    public event System.Action OnPlaced;

    public bool IsPlaced => isPlaced;

    private bool isPlaced;
    private DefibrilatorTargetEmplacement emplacement;
    protected override void Start() {
        base.Start();
        OnAttach += Remove;
    }

    private void Remove() {
        if(!isPlaced) return;
        emplacement.gameObject.SetActive(true);
        emplacement = null;
        isPlaced = false;
    }

    private void OnTriggerEnter(Collider other) {
        Detach(true);
        transform.DOMove(other.transform.position, .1f);
        transform.DORotateQuaternion(other.transform.rotation, .1f).OnComplete(() => Place(other.gameObject));
    }

    private void Place(GameObject go) {
        isPlaced = true;
        emplacement = go.GetComponent<DefibrilatorTargetEmplacement>();
        emplacement.gameObject.SetActive(false);
        OnPlaced?.Invoke();
    }
}
