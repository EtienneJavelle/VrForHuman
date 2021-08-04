using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(InteractableHoverEvents))]
public class DefibrilatorSticker : MonoBehaviour {
    [SerializeField] private Interactable interactable;
    [SerializeField] private InteractableHoverEvents interactableHover;

    private void Start() {
        interactableHover.onHandHoverBegin.AddListener(() => Debug.Log("Hand Hover Begin"));
    }
}
