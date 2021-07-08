using UnityEngine;
using Valve.VR.InteractionSystem;

public class Hemorrhage : MonoBehaviour {
    [SerializeField] private Interactable interactable;
    [SerializeField] private GameObject particle;

    private void Start() {
        interactable.onAttachedToHand += _ => particle.SetActive(false);
        interactable.onDetachedFromHand += _ => particle.SetActive(true);
    }
}
