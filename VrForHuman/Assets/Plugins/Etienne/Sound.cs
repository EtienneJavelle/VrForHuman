using UnityEngine;
using UnityEngine.Audio;
namespace Etienne {
    [System.Serializable]
    public class Sound {
        public string Name => name;
        public AudioClip Clip => clip;
        public AudioMixerGroup Output => output;
        public float Volume => volume;
        public float Pitch => pitch;
        public bool Loop => loop;

        [SerializeField] private string name;
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioMixerGroup output;
        [Range(0, 1)]
        [SerializeField] private float volume = 1;
        [Range(.1f, 3)]
        [SerializeField] private float pitch = 1;
        [SerializeField] private bool loop;

    }
}