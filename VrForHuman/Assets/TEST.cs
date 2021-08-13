using Etienne;
using UnityEngine;

[Requirement(typeof(AudioManager))]
public class TEST : MonoBehaviourWithRequirement {
    public TMPro.VertexGradient uuu;
    public Cue cue = new Cue(null);

    [ContextMenu("TEST")]
    private void MyMethod() {
        AudioManager.Play(cue);
    }

}
