using Etienne;
using UnityEngine;
using UnityEngine.UI;

[Requirement(typeof(UIAudioManager))]
public class TEST : MonoBehaviourWithRequirement {
    public TMPro.VertexGradient uuu;

    [ContextMenu("TEST")]
    private void MyMethod() {
        Debug.Log(UIAudioManager.Instance.gameObject.name);
        Debug.Log(AudioManager.Instance.gameObject.name);
    }

}
