using UnityEngine;

[RequireComponent(typeof(InteractableSticker))]
public class Pouch : MonoBehaviour {
    public bool IsOpen => isOpen;

    private DefibrilatorElectrodes[] electrodes;
    private InteractableSticker sticker;
    private Defibrilator defibrilator;
    private bool isOpen;

    private void Awake() {
        defibrilator = GetComponentInParent<Defibrilator>();
        sticker = GetComponent<InteractableSticker>();
        sticker.OnAttach += EnableElectrodes;
    }

    private void Start() {
        electrodes = defibrilator.Electrodes;
    }

    private void EnableElectrodes() {
        isOpen = true;
        defibrilator.SetActiveElectrodes(true);
    }
}
