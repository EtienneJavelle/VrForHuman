using DG.Tweening;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(InteractableHoverEvents))]
public class DefibrilatorSticker : MonoBehaviour {
    [SerializeField] private Interactable interactable;
    [SerializeField] private InteractableHoverEvents interactableHover;
    [SerializeField] private float velocityThreshold = 1.1f;
    private bool isAttached;
    private Hand hand;
    private Rigidbody rb;
    private Vector3 oldVelocity;

    private void Start() {
        rb = GetComponent<Rigidbody>();

        interactableHover.onHandHoverBegin.AddListener(HandHoverBegin);
    }
    private void Update() {
        if(isAttached) {
            Vector3 velocity = hand.GetTrackedObjectVelocity();
            if(velocity.sqrMagnitude > velocityThreshold) {
                //if(velocity.sqrMagnitude < velocityThreshold && oldVelocity.sqrMagnitude > velocityThreshold) {
                Detach(velocity);
            }
            oldVelocity = velocity;
        }
    }

    private void Attach() {
        if(enabled) {
            hand = interactable.hoveringHand;
            rb.isKinematic = true;
            transform.parent = hand.transform;
            transform.DOLocalMove(hand.objectAttachmentPoint.localPosition, interactable.snapAttachEaseInTime * (interactable.attachEaseIn ? 1 : 0));
            isAttached = true;
        }
    }

    private void Detach(bool isKinematic = false) {
        Detach(Vector3.zero, isKinematic);
    }

    private void Detach(Vector3 velocity, bool isKinematic = false) {
        if(enabled) {
            transform.parent = null;
            rb.isKinematic = isKinematic;
            rb.velocity = velocity;
            hand = null;
            isAttached = false;
        }
    }

    public void HandHoverBegin() {
        Debug.Log($"Hand Hover Begin {hand}", hand);
        Attach();
    }

    private void OnTriggerEnter(Collider other) {
        Detach(true);
        transform.DOMove(other.transform.position, .1f);
        transform.DORotateQuaternion(other.transform.rotation, .1f);
    }
}
