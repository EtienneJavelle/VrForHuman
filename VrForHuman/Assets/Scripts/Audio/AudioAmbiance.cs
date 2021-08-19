using Etienne;
using UnityEngine;

[Requirement(typeof(AudioManager))]
public class AudioAmbiance : MonoBehaviourWithRequirement {
    [SerializeField] private Sound[] ambiantSounds = new Sound[] { new Sound(null) };

    protected virtual void Start() {
        foreach(Sound sound in ambiantSounds) {
            AudioManager.Play(sound);
        }
    }
}
