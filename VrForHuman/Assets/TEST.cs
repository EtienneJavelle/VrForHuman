using UnityEngine;

public class TEST : MonoBehaviour {
    private void OnDestroy() {
        Debug.Log("<color=red>Destroyed</color>", transform.parent.gameObject);
    }
    private void OnDisable() {
        Debug.Log("<color=red>Disabled</color>", gameObject);
    }

    private void OnEnable() {
        Debug.Log("<color=green>Enabled</color>", transform.parent.gameObject);
    }

}
