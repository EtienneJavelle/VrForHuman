using UnityEngine;

namespace Etienne {
    [System.Serializable]
    public struct SoundParameters {
        public float Volume => volume;
        public float Pitch => pitch;
        public bool Loop => loop;

        [Range(0, 1)]
        [SerializeField] private float volume;
        [Range(.1f, 3)]
        [SerializeField] private float pitch;
        [SerializeField] private bool loop;

        public SoundParameters(float volume, float pitch, bool loop = false) {
            this.volume = volume;
            this.pitch = pitch;
            this.loop = loop;
        }
    }
}