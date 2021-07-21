using Etienne;
using UnityEngine;
using UnityEngine.UI;

[Requirement(typeof(AudioManager2D))]
public class TEST : MonoBehaviourWithRequirement {
    public TMPro.VertexGradient uuu;
    public Cue cue = new Cue(null);

    [ContextMenu("TEST")]
    private void MyMethod() {
        AudioManager2D.Play(cue);
    }

}
