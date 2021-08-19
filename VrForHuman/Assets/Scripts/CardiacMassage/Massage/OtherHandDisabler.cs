using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(InteractableHoverEvents))]
public class OtherHandDisabler : MonoBehaviour {
    [SerializeField] private Interactable interactable;
    [SerializeField] private InteractableHoverEvents interactableHover;

    private Hand otherHand;

    [ContextMenu("CashFields")]
    private void CashFields() {
        interactable ??= GetComponent<Interactable>();
        interactableHover ??= GetComponent<InteractableHoverEvents>();
    }

    private void Start() {
        interactableHover.onHandHoverBegin.AddListener(HandHoverBegin);
        interactableHover.onHandHoverEnd.AddListener(HandHoverEnd);
    }

    private void HandHoverBegin() {
        otherHand = interactable.hoveringHand.otherHand;
        otherHand.useHoverSphere = false;
    }
    private void HandHoverEnd() {
        otherHand.useHoverSphere = true;
    }
    private void OnDisable() {
        HandHoverEnd();
    }
}
