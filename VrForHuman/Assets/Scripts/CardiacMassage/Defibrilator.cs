using DG.Tweening;
using Etienne;
using UnityEngine;

[Requirement(typeof(GameManager))]
[RequireComponent(typeof(Path))]
public class Defibrilator : MonoBehaviourWithRequirement {
    [SerializeField] private float waitingTime = 3f, pathDuration = 3f;

    private Path path;

    private void Start() {
        path = GetComponent<Path>();
        Sequence sequence = DOTween.Sequence();
        Transform body = transform.GetChild(0);
        body.position = path.WorldWaypoints[0];
        sequence.Append(body.DOMove(body.position, waitingTime));
        sequence.Append(body.DOPath(path.WorldWaypoints, pathDuration));
        sequence.Play();
    }
}
