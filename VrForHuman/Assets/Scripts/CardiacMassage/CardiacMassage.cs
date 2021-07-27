using System;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace CardiacMassage {
    [Etienne.Requirement(typeof(GameManager))]
    public class CardiacMassage : Etienne.MonoBehaviourWithRequirement {
        public event Action OnMassageStart, OnMassageStop;
        public event Action OnPressureBegin;
        public event Action<CardiacMassagePressureData> OnPressureDone;

        [SerializeField] private float pressureDepth;
        [SerializeField] private State state;
        [SerializeField] private Interactable interactable;
        [SerializeField] private HoverButton hoverButton;
        [SerializeField] private AnimationCurve pushes;
        [SerializeField, Range(0, 1)] private float pushThreshold = .1f;
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
        public List<CardiacMassagePressureData> pushDatas { get; protected set; }

        private void Awake() {
            if(GameManager.Instance.CardiacMassage == null) {
                GameManager.Instance.SetCardiacMassage(this);
                pushDatas = new List<CardiacMassagePressureData>();
            }
            hoverButton ??= GetComponent<HoverButton>();
            interactable ??= GetComponent<Interactable>();
            interactable.onAttachedToHand += _ => StartMassage();
            interactable.onDetachedFromHand += _ => StopMassage();
        }
        private void Start() {
            StartMassage();
        }
        private void Update() {
            if(isStarted) {
                TestChestBone.localScale = new Vector3(1, 1,
                    Mathf.Min(hoverButton.movingPart.position.y, transform.position.y) / transform.position.y);
            }

            Vector3 currentPosition = hoverButton.movingPart.position;
            velocity = currentPosition - oldPosition;
            float verticalVeloxuty = velocity.y / Time.deltaTime;
            isGoingDown = verticalVeloxuty < -pushThreshold;
            isGoingUp = verticalVeloxuty > pushThreshold;
            if(isGoingDown) {
                maxPosition = currentPosition;
                SetState(State.Down);
                Debug.DrawLine(currentPosition, currentPosition + velocity.normalized, Color.red);
            }
            if(isGoingUp) {
                minPosition = currentPosition;
                SetState(State.Up);
                Debug.DrawLine(currentPosition, currentPosition + velocity.normalized, Color.green);
            }
            oldPosition = currentPosition;
        }

        [ContextMenu("Start Massage")]
        private void StartMassage() {
            isStarted = true;
            startPosition = hoverButton.movingPart.position;
            oldPosition = startPosition;
            startMassageTime = Time.realtimeSinceStartup;
            OnMassageStart?.Invoke();
        }

        [ContextMenu("Stop Massage")]
        private void StopMassage() {
            isStarted = false;
            OnMassageStop?.Invoke();
        }

        private void SetState(State state) {
            if(state == this.state) {
                return;
            }

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
                    pressureDepth = -Mathf.Abs(maxPosition.sqrMagnitude - startPosition.sqrMagnitude) * 10;
                    CardiacMassagePressureData push = new CardiacMassagePressureData(pressureDepth, Time.realtimeSinceStartup - startMassageTime);
                    pushDatas.Add(push);

                    Debug.Log(push.Depth);
                    Debug.Log(push.Time);
                    keyframe = new Keyframe(push.Time, push.Depth, 0, 0, 0, 0);
                    pushes.AddKey(keyframe);

                    OnPressureDone?.Invoke(push);
                    break;
                case State.Down:
                    OnPressureBegin?.Invoke();
                    minPosition = hoverButton.movingPart.position;
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
}

