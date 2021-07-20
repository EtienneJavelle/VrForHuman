using CardiacMassage;
using UnityEngine;

public class TestDebug : MonoBehaviour {
    private VRInputModule vRInputModule;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    private void Start() {
        vRInputModule = FindObjectOfType<VRInputModule>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    private void Update() {
#if UNITY_EDITOR

        if(vRInputModule != null && Input.GetKeyDown(KeyCode.C)) {
            vRInputModule.ClassicMode();
        }

        if(vRInputModule != null && Input.GetKeyDown(KeyCode.V)) {
            vRInputModule.ArcadeMode();
        }

        if(Input.GetKeyDown(KeyCode.I) && scoreManager != null) {
            scoreManager.ChangeScore(1000);
        }

        if(Input.GetKeyDown(KeyCode.K) && scoreManager != null) {
            scoreManager.ChangeScore(1000);
        }

        if(Input.GetKeyDown(KeyCode.P)) {
            GameManager.Instance.EndGame();
        }
#endif
    }
}
