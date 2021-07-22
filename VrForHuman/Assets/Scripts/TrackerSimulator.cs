using UnityEngine;

//todo verifier si on en a encore besoin
public class TrackerSimulator : MonoBehaviour {
    public GameObject bodyTracker, rightHandTracker, leftHandTracker;
    public GameObject bodyBones, rightHandBones, leftHandBones;

    private void Update() {
        bodyBones.transform.position = bodyTracker.transform.position;
        rightHandBones.transform.position = rightHandTracker.transform.position;
        leftHandBones.transform.position = leftHandTracker.transform.position;
    }
}
