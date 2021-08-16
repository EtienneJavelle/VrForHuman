using DG.Tweening;
using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(InteractableHoverEvents))]
public class InteractableSticker : MonoBehaviour {
    public event Action OnAttach, OnDetach;

    [SerializeField] private Interactable interactable;
    [SerializeField] private InteractableHoverEvents interactableHover;
    [SerializeField] private float velocityThreshold = 1.1f;
    private bool isAttached;
    private Hand hand;
    private Rigidbody rb;

    protected virtual void Start() {
        rb = GetComponent<Rigidbody>();
        interactableHover.onHandHoverBegin.AddListener(HandHoverBegin);
        SceneLoader.Instance.OnSceneChanged += Kill;
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
        if(enabled) {
            hand = interactable.hoveringHand;
            rb.isKinematic = true;
            transform.parent = hand.transform;
            transform.DOLocalMove(hand.objectAttachmentPoint.localPosition, interactable.snapAttachEaseInTime * (interactable.attachEaseIn ? 1 : 0));
            isAttached = true;
            OnAttach?.Invoke();
        }
    }

    protected void Detach(bool isKinematic = false) {
        Detach(Vector3.zero, isKinematic);
    }

    protected void Detach(Vector3 velocity, bool isKinematic = false) {
        if(enabled) {
            transform.parent = null;
            rb.isKinematic = isKinematic;
            rb.velocity = velocity;
            hand = null;
            isAttached = false;
            OnDetach?.Invoke();
        }
    }

    private void HandHoverBegin() {
        Debug.Log($"Hand Hover Begin {hand}", hand);
        Attach();
    }
}
