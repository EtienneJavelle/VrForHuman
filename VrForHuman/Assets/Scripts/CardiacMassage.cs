using System;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

internal class MyClass {
    private CardiacMassage g;

    private void uuu() {
        //g.
    }
}
public class CardiacMassage : MonoBehaviour {
    public event Action OnPressureBegin;
    public event Action<CardiacMassagePressureData> OnPressureDone;

    [SerializeField] private float pressureDepth;
    [SerializeField] private float beat = 500;
    [SerializeField] private State state;
    [SerializeField] private Interactable interactable;
    [SerializeField] private AnimationCurve pushes;
    [Tooltip("TEST")]
    [SerializeField] private Transform TestChestBone;

    private enum State { Idle, Up, Down }
    private bool isStarted;
    private Vector3 startPosition;
    private Vector3 minPosition;
    private Vector3 maxPosition;
    private Vector3 velocity;
    private Vector3 oldPosition;
    private bool isGoingUp, isGoingDown;
    private float startMassageTime;
    private List<CardiacMassagePressureData> pushDatas = new List<CardiacMassagePressureData>();

    private void Awake() {
        interactable ??= GetComponent<Interactable>();
        interactable.onAttachedToHand += _ => StartMassage();
        interactable.onDetachedFromHand += _ => StopMassage();

        OnPressureBegin += () => print("Begin");
        OnPressureDone += push => print(push.Depth);
    }

    private void Update() {
        if(isStarted) {
            bool wasGoingDown = isGoingDown;
            bool wasGoingUp = isGoingUp;
            velocity = (transform.position - oldPosition) / Time.deltaTime;
            isGoingDown = velocity.y < 0;
            isGoingUp = velocity.y > 0;

            if(isGoingDown) {
                SetState(State.Down);
            } else if(isGoingUp) {
                SetState(State.Up);
            } else {
                // SetState(State.Idle);
            }
            if(isGoingDown && transform.position.y < startPosition.y) {
                maxPosition = transform.position;
            }
            TestChestBone.localScale = new Vector3(1, 1, Mathf.Min(transform.position.y, startPosition.y) / startPosition.y);
            //for(int i = 0; i < TestChestBone.GetChild(0).childCount; i++) {
            //    Transform child = TestChestBone.GetChild(0).GetChild(i);
            //    child.transform.localScale = new Vector3(
            //    child.transform.localScale.x / TestChestBone.localScale.x,
            //    child.transform.localScale.y / TestChestBone.localScale.y,
            //    child.transform.localScale.z / TestChestBone.localScale.z);

            //}


            oldPosition = transform.position;
        }

    }

    [ContextMenu("Start Massage")]
    private void StartMassage() {
        isStarted = true;
        startPosition = transform.position;
        startMassageTime = Time.realtimeSinceStartup;
    }

    [ContextMenu("Stop Massage")]
    private void StopMassage() {

        isStarted = false;
    }

    private void SetState(State state) {
        if(state == this.state)
            return;

        Debug.Log($"Changed state from {this.state} to {state}");

        ExitState();

        EnterState(state);

        this.state = state;
    }

    private void EnterState(State state) {
        Keyframe keyframe;
        switch(state) {
            case State.Idle:
                break;
            case State.Up:
                pressureDepth =
                    -Mathf.Abs(maxPosition.sqrMagnitude - startPosition.sqrMagnitude)
                    //-Mathf.Min(
                    //Mathf.Abs(maxPosition.sqrMagnitude - startPosition.sqrMagnitude),
                    //Mathf.Abs(maxPosition.sqrMagnitude - minPosition.sqrMagnitude)
                    //)
                    * 10;
                CardiacMassagePressureData push = new CardiacMassagePressureData(pressureDepth, Time.realtimeSinceStartup - startMassageTime);
                pushDatas.Add(push);
                keyframe = new Keyframe(push.Time, push.Depth, 0, 0, 0, 0);
                pushes.AddKey(keyframe);
                OnPressureDone?.Invoke(push);
                break;
            case State.Down:
                OnPressureBegin?.Invoke();
                minPosition = transform.position;
                keyframe = new Keyframe(Time.realtimeSinceStartup - startMassageTime,
                   Mathf.Min(
                   Mathf.Abs(startPosition.sqrMagnitude),
                   Mathf.Abs(minPosition.sqrMagnitude)
                   ) * 10,
                   0, 0, 0, 0);
                pushes.AddKey(keyframe);
                break;
        }
    }

    private void ExitState() {
        switch(state) {
            case State.Idle:
                break;
            case State.Up:
                break;
            case State.Down:
                break;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        if(isStarted) {
            Gizmos.DrawWireSphere(startPosition, .05f);
            Gizmos.DrawSphere(maxPosition, .05f);
            Gizmos.DrawSphere(minPosition, .05f);
            Gizmos.DrawLine(startPosition, maxPosition);
        }
    }


}
