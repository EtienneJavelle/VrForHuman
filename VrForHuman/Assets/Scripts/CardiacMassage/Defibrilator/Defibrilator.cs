using DG.Tweening;
using Etienne;
using UnityEditor;
using UnityEngine;

[Requirement(typeof(GameManager))]
[RequireComponent(typeof(Path))]
public class Defibrilator : MonoBehaviourWithRequirement {
    [SerializeField] private Lid lid;
    [SerializeField] private float waitingTime = 3f, pathDuration = 3f;

    private Path path;
    private DefibrilatorTargetEmplacement[] targets;

    private void Awake() {
        path = GetComponent<Path>();
        targets = GetComponentsInChildren<DefibrilatorTargetEmplacement>();
        lid ??= GetComponentInChildren<Lid>();
    }

    private void Start() {
        GameObject parent = new GameObject("DefibrilatorTargets");
        foreach(DefibrilatorTargetEmplacement target in targets) {
            EditorGUIUtility.PingObject(target);
            Debug.Log("target Found ~!", target);
            target.transform.parent = parent.transform;
            target.gameObject.SetActive(false);
        }
        Sequence sequence = DOTween.Sequence();
        Transform body = transform.GetChild(0);
        body.position = path.WorldWaypoints[0];
        sequence.Append(body.DOMove(body.position, waitingTime));
        sequence.Append(body.DOPath(path.WorldWaypoints, pathDuration));
        sequence.Append(lid.Open().OnComplete(EnableTargets));
        sequence.Play();
    }

    private void EnableTargets() {
        foreach(DefibrilatorTargetEmplacement target in targets) {
            target.gameObject.SetActive(true);
            target.Enable();
        }
    }
}
