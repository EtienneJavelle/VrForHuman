using System.Collections.Generic;
using Etienne;
using UnityEngine.Audio;

namespace UnityEngine.UI {
    [AddComponentMenu("Audio/Audio Managers/Audio Manager 2D")]
    public class AudioManager2D : Singleton<AudioManager2D> {
        [SerializeField] private AudioMixerGroup output;

        public static void Play(Sound sound) {
            AudioSource source = Instance.FindFreeAudiosource(Instance.audioSources);
            source.outputAudioMixerGroup = Instance.output;
            source.SetSoundToSource(sound);
            source.Play();
        }

        private List<AudioSource> audioSources = new List<AudioSource>();
    }
}
