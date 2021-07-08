using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerSimulator : MonoBehaviour
{
    public GameObject bodyTracker, rightHandTracker, leftHandTracker;
    public GameObject bodyBones, rightHandBones, leftHandBones;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bodyBones.transform.position = bodyTracker.transform.position;
        rightHandBones.transform.position = rightHandTracker.transform.position;
        leftHandBones.transform.position = leftHandTracker.transform.position;
    }
}
