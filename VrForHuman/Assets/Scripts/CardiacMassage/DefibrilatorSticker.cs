using DG.Tweening;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(InteractableHoverEvents))]
public class DefibrilatorSticker : MonoBehaviour {
    [SerializeField] private Interactable interactable;
    [SerializeField] private InteractableHoverEvents interactableHover;
    [SerializeField] private float velocityThreshold = 1.1f;

    private Hand hand;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();

        interactableHover.onHandHoverBegin.AddListener(HandHoverBegin);
    }
    private void Update() {
        if(hand != null) {
            Vector3 velocity = hand.GetTrackedObjectVelocity();
            if(velocity.sqrMagnitude > velocityThreshold) {
                transform.parent = null;
                rb.isKinematic = false;
                rb.velocity = velocity;
            }
        }
    }

    public void HandHoverBegin() {
        hand = interactable.hoveringHand;
        Debug.Log($"Hand Hover Begin {hand}", hand);
        rb.isKinematic = true;
        transform.parent = hand.transform;
    }
    private void OnTriggerEnter(Collider other) {
        transform.parent = null;
        transform.DOMove(other.transform.position, .1f);
        transform.DORotateQuaternion(other.transform.rotation, .1f);
    }
}
