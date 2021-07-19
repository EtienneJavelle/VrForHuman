using UnityEngine;
using UnityEngine.EventSystems;

public class VRPointer : MonoBehaviour {
    [SerializeField] private float defaultLenght = 5f;
    [SerializeField] private GameObject dot;
    [SerializeField] private VRInputModule inputModule;
    private LineRenderer lineRenderer;

    private void Awake() {
        lineRenderer ??= GetComponent<LineRenderer>();
    }

    private void Update() {
        UpdateLine();
    }

    private void UpdateLine() {
        //defaul or distance
        PointerEventData data = inputModule.GetData;
        float targetLenght = data.pointerCurrentRaycast.distance == 0 ? defaultLenght : data.pointerCurrentRaycast.distance;

        //Raucast
        RaycastHit hit = CreateRaycast(targetLenght);
        //Default
        Vector3 endPosition = transform.position + (transform.forward * targetLenght);
        //Based on hit
        if(hit.collider != null) {
            endPosition = hit.point;
        }

        //set pos to the dot
        dot.transform.position = endPosition;

        //set LineRenderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float lenght) {
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out RaycastHit hit, defaultLenght);
        return hit;
    }
}
