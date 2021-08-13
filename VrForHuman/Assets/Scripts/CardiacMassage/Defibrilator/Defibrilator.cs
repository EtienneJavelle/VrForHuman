using DG.Tweening;
using Etienne;
using System;
using UnityEngine;

namespace CardiacMassage {
    [Requirement(typeof(GameManager))]
    [RequireComponent(typeof(Path), typeof(Defibrilation))]
    public class Defibrilator : MonoBehaviourWithRequirement {
        public DefibrilatorElectrodes[] Electrodes => electrodes;

        [SerializeField] private Lid lid;
        [SerializeField] private Pouch pouch;
        [SerializeField] private float waitingTime = 3f, pathDuration = 3f;

        private Path path;
        private DefibrilatorTargetEmplacement[] targets;
        private DefibrilatorElectrodes[] electrodes;
        private Defibrilation defibrilation;
        private GameObject parent;

        private void Awake() {
            path = GetComponent<Path>();
            targets = GetComponentsInChildren<DefibrilatorTargetEmplacement>();
            electrodes = GetComponentsInChildren<DefibrilatorElectrodes>();
            foreach(DefibrilatorElectrodes electrode in electrodes) {
                electrode.OnPlaced += CheckIfBothAreConnected;
                electrode.OnPlaced += DisableEmplacement;
                electrode.OnAttach += CheckIfBothAreConnected;
                electrode.OnAttach += EnableEmplacement;
            }
            lid ??= GetComponentInChildren<Lid>();
            lid.OnOpenLid += EnableDefibrilator;
            lid.OnCloseLid += DisableDefibrilator;
            pouch ??= GetComponentInChildren<Pouch>();
            defibrilation = GetComponent<Defibrilation>();
        }

        private void EnableEmplacement() {
            throw new NotImplementedException();
        }

        private void DisableEmplacement() {
            throw new NotImplementedException();
        }

        private void Start() {
            DisableDefibrilator();
        }

        private void OnEnable() {
            Sequence sequence = DOTween.Sequence();
            Transform body = transform.GetChild(0);
            body.position = path.WorldWaypoints[0];
            sequence.AppendInterval(waitingTime);
            sequence.Append(body.DOPath(path.WorldWaypoints, pathDuration));
            sequence.Play().OnComplete(SetupTargets);
        }

        private void SetupTargets() {
            parent ??= new GameObject("DefibrilatorTargets");
            foreach(DefibrilatorTargetEmplacement target in targets) {
                target.transform.parent = parent.transform;
            }
        }

        private void DisableDefibrilator() {
            SetActiveTargets(false);
            SetActiveElectrodes(false);
            pouch.gameObject.SetActive(false);
        }

        private void EnableDefibrilator() {
            SetActiveTargets(true);
            if(pouch.IsOpen) {
                SetActiveElectrodes(true);
            }
            pouch.gameObject.SetActive(true);
        }

        private void SetActiveTargets(bool activate) {
            foreach(DefibrilatorTargetEmplacement target in targets) {
                target.gameObject.SetActive(activate);
            }
        }

        public void SetActiveElectrodes(bool activate) {
            foreach(DefibrilatorElectrodes electrode in electrodes) {
                electrode.gameObject.SetActive(activate);
            }
        }

        private void CheckIfBothAreConnected() {
            int count = 0;
            foreach(DefibrilatorElectrodes electrode in electrodes) {
                if(electrode.IsPlaced) {
                    count++;
                }
            }
            if(count == electrodes.Length) {
                //todo  Start Defibrilator Sound=>actions
                if(defibrilation.StartDefibrilation()) {
                    foreach(DefibrilatorElectrodes electrode in electrodes) {
                        electrode.enabled = false;
                    }
                }
                Debug.Log("Both Are Connected !");
            } else {
                defibrilation.PauseDefibrilation();
                Debug.Log("Only one is connected.");
            }
        }
    }
}