using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(InteractableHoverEvents))]
public class InteractableSticker : MonoBehaviour {
    public event Action OnAttach, OnDetach;

    [Header("Sticker")]
    [SerializeField] protected Interactable interactable;
    [SerializeField] private InteractableHoverEvents interactableHover;
    [SerializeField] private float velocityThreshold = 1.1f;
    protected Hand hand;
    protected Rigidbody rb;
    private BoxCollider otherHandCollider;
    private bool isAttached;

    [ContextMenu("Cash Fields")]
    protected virtual void CashFields() {
        interactable = GetComponent<Interactable>();
        interactableHover = GetComponent<InteractableHoverEvents>();
    }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody>();
        interactableHover.onHandHoverBegin.AddListener(HandHoverBegin);
        if(SceneLoader.Instance != null) {
            SceneLoader.Instance.OnSceneChanged += Kill;
        } else {
            Debug.LogWarning($"There is no SceneLoader");
        }
    }

    private void Kill() {
        SceneLoader.Instance.OnSceneChanged -= Kill;
        GameObject.Destroy(gameObject);
    }

    private void Update() {
        if(isAttached) {
            Vector3 velocity = hand.GetTrackedObjectVelocity();
            if(velocity.sqrMagnitude > velocityThreshold) {
                Detach(velocity);
            }
        }
    }

    private void Attach() {
        if(enabled && !isAttached) {
            hand = interactable.hoveringHand;
            otherHandCollider = hand.otherHand.GetComponent<BoxCollider>();
            otherHandCollider.enabled = true;
            rb.isKinematic = true;
            transform.parent = hand.transform;
            transform.localPosition = hand.objectAttachmentPoint.localPosition;
            isAttached = true;
            OnAttach?.Invoke();
        }
    }

    protected void Detach(bool isKinematic = false) {
        Detach(Vector3.zero, isKinematic);
    }

    protected void Detach(Vector3 velocity, bool isKinematic = false) {
        if(enabled) {
            OnDetach?.Invoke();
            otherHandCollider.enabled = false;
            transform.parent = null;
            rb.isKinematic = isKinematic;
            rb.velocity = velocity;
            isAttached = false;
            hand = null;
            otherHandCollider = null;
        }
    }

    private void HandHoverBegin() {
        Debug.Log($"Hand Hover Begin {hand}", hand);
        Attach();
    }
}
