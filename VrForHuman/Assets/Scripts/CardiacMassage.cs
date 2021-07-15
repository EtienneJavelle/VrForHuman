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
    public event Action OnPressureDone;

    [SerializeField] private float pressureDepth;
    [SerializeField] private float beat = 500;
    [SerializeField] private State state;
    [SerializeField] private Interactable interactable;
    [SerializeField] private AnimationCurve pushes;

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
    }

    private void Update() {
        if(isStarted) {
            bool wasGoingDown = isGoingDown;
            bool wasGoingUp = isGoingUp;
            velocity = (transform.position - oldPosition) / Time.deltaTime;
            isGoingDown = velocity.y < 0;
            isGoingUp = velocity.y > 0;

            if(velocity.y < 0) {
                SetState(State.Down);
            } else if(velocity.y > 0) {
                SetState(State.Up);
            } else {
                SetState(State.Idle);
            }
            if(isGoingDown && transform.position.y < startPosition.y) {
                maxPosition = transform.position;
            }

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
        switch(state) {
            case State.Idle:
                break;
            case State.Up:
                OnPressureDone?.Invoke();
                pressureDepth =
                    -Mathf.Abs(maxPosition.sqrMagnitude - startPosition.sqrMagnitude)
                    //-Mathf.Min(
                    //Mathf.Abs(maxPosition.sqrMagnitude - startPosition.sqrMagnitude),
                    //Mathf.Abs(maxPosition.sqrMagnitude - minPosition.sqrMagnitude)
                    //)
                    * 10;
                CardiacMassagePressureData push = new CardiacMassagePressureData(pressureDepth, Time.realtimeSinceStartup - startMassageTime);
                pushDatas.Add(push);
                Keyframe keyframe = new Keyframe(push.Time, push.Depth, 0, 0, 0, 0);
                pushes.AddKey(keyframe);
                break;
            case State.Down:
                break;
        }
    }

    private void ExitState() {
        switch(state) {
            case State.Idle:
                break;
            case State.Up:
                OnPressureBegin?.Invoke();
                minPosition = transform.position;
                Keyframe keyframe = new Keyframe(Time.realtimeSinceStartup - startMassageTime,
                    Mathf.Min(
                    Mathf.Abs(startPosition.sqrMagnitude),
                    Mathf.Abs(minPosition.sqrMagnitude)
                    ) * 10,
                    0, 0, 0, 0);
                pushes.AddKey(keyframe);
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

[System.Serializable]
public struct CardiacMassagePressureData {
    public float Time => time;
    public float Depth => depth;

    [SerializeField] private float depth;
    [SerializeField] private float time;

    public CardiacMassagePressureData(float depth, float time) {
        this.depth = depth;
        this.time = time;
    }
}