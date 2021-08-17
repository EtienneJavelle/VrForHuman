using DG.Tweening;
using Etienne;
using UnityEngine;

namespace CardiacMassage {
    [Requirement(typeof(GameManager))]
    [RequireComponent(typeof(Path), typeof(Defibrilation))]
    public class Defibrilator : MonoBehaviourWithRequirement {
        public DefibrilatorElectrodes[] Electrodes => electrodes;

        [SerializeField] private Lid lid;
        [SerializeField] private Pouch pouch;
        [SerializeField] private float waitingTime = 3f, pathDuration = 3f;
        [SerializeField]
        private Etienne.Sound enableTargetSound = new Sound(null),
            disableTargetSound = new Sound(null);

        private Path path;
        private DefibrilatorTargetEmplacement[] targets;
        private DefibrilatorElectrodes[] electrodes;
        private Defibrilation defibrilation;
        private GameObject parent;
        private bool AreBothElectrodesPlaced => electrodes[0].IsPlaced && electrodes[1].IsPlaced;

        private void Awake() {
            path = GetComponent<Path>();
            targets = GetComponentsInChildren<DefibrilatorTargetEmplacement>();
            electrodes = GetComponentsInChildren<DefibrilatorElectrodes>();
            foreach(DefibrilatorElectrodes electrode in electrodes) {
                electrode.OnPlaced += HandleDefibrilation;
                electrode.OnPlaced += DisableEmplacement;
                electrode.OnAttach += HandleDefibrilation;
                electrode.OnAttach += EnableEmplacement;
            }
            lid ??= GetComponentInChildren<Lid>();
            lid.OnOpenLid += EnableDefibrilator;
            lid.OnCloseLid += DisableDefibrilator;
            pouch ??= GetComponentInChildren<Pouch>();
            defibrilation = GetComponent<Defibrilation>();
        }

        private void EnableEmplacement() {
            AudioManager.Play(enableTargetSound);
            SetActiveTargets(true);
        }

        private void DisableEmplacement() {
            if(AreBothElectrodesPlaced) {
                AudioManager.Play(disableTargetSound);
                SetActiveTargets(false);
            }
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

        private void HandleDefibrilation() {
            if(AreBothElectrodesPlaced) {
                if(defibrilation.StartDefibrilation()) {
                    foreach(DefibrilatorElectrodes electrode in electrodes) {
                        electrode.enabled = false;
                    }
                }
            } else {
                defibrilation.PauseDefibrilation();
            }
        }
    }
}