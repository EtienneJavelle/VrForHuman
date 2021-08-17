using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


[RequireComponent(typeof(BoxCollider))]
public class HandController : MonoBehaviour {
    private Hand hand;

    private float palmRadius;
    private Transform palm;

    private SteamVR_Behaviour_Skeleton Skeleton => skeleton ??= GetComponentInChildren<SteamVR_Behaviour_Skeleton>();
    private Transform FingerTip => fingerTip ??= Skeleton.GetBone((int)hand.fingerJointHover);

    private SteamVR_Behaviour_Skeleton skeleton;
    private Transform fingerTip;

    private void Awake() {
        hand = GetComponent<Hand>();
        palm = hand.hoverSphereTransform;
        palmRadius = hand.hoverSphereRadius;
    }

    [ContextMenu("Finger")]
    private void Finger() {
        if(FingerTip == null) return;
        hand.hoverSphereTransform = FingerTip;
        hand.hoverSphereRadius = hand.fingerJointHoverRadius;
    }
    [ContextMenu("Palm")]
    private void Palm() {
        hand.hoverSphereTransform = palm;
        hand.hoverSphereRadius = palmRadius;
    }

    private void OnTriggerEnter(Collider other) {
        Finger();
    }

    private void OnTriggerExit(Collider other) {
        Palm();
    }
}
