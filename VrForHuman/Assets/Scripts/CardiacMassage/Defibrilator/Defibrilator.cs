using DG.Tweening;
using Etienne;
using UnityEngine;

[Requirement(typeof(GameManager))]
[RequireComponent(typeof(Path))]
public class Defibrilator : MonoBehaviourWithRequirement {
    public DefibrilatorElectrodes[] Electrodes => electrodes;

    [SerializeField] private Lid lid;
    [SerializeField] private Pouch pouch;
    [SerializeField] private float waitingTime = 3f, pathDuration = 3f;

    private Path path;
    private DefibrilatorTargetEmplacement[] targets;
    private DefibrilatorElectrodes[] electrodes;

    private void Awake() {
        path = GetComponent<Path>();
        targets = GetComponentsInChildren<DefibrilatorTargetEmplacement>();
        electrodes = GetComponentsInChildren<DefibrilatorElectrodes>();
        foreach(DefibrilatorElectrodes electrode in electrodes) {
            electrode.OnPlaced += CheckIfBothAreConnected;
        }
        lid ??= GetComponentInChildren<Lid>();
        lid.OnOpenLid += EnableDefibrilator;
        lid.OnCloseLid += DisableDefibrilator;
        pouch ??= GetComponentInChildren<Pouch>();
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
        GameObject parent = new GameObject("DefibrilatorTargets");
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
            Debug.Log("Both Are Connected !");
        } else {
            Debug.Log($"Only one is connected.");
        }
    }
}
