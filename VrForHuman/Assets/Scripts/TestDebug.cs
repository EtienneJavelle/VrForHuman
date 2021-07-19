using UnityEngine;

public class TestDebug : MonoBehaviour {
    private VRInputModule vRInputModule;

    // Start is called before the first frame update
    private void Start() {
        vRInputModule = FindObjectOfType<VRInputModule>();
    }

    // Update is called once per frame
    private void Update() {
        if(vRInputModule != null && Input.GetKeyDown(KeyCode.C)) {
            vRInputModule.ClassicMode();
        }

        if(vRInputModule != null && Input.GetKeyDown(KeyCode.V)) {
            vRInputModule.ArcadeMode();
        }
    }
}
