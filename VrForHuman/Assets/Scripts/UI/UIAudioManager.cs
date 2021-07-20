using System.Collections.Generic;
using Etienne;

namespace UnityEngine.UI {
    [AddComponentMenu("Audio/Audio Managers/UI Audio Manager")]
    public class UIAudioManager : Singleton<UIAudioManager> {

        protected override void Awake() {
            base.Awake();

            audioSources.Add(gameObject.AddComponent<AudioSource>());
        }

        public static void Play(Sound sound) {
            AudioSource source = Instance.FindFreeAudiosource();
            SetSoundToSource(sound, source);
            source.Play();
        }

        private static void SetSoundToSource(Sound sound, AudioSource source) {
            source.clip = sound.Clip;
            source.outputAudioMixerGroup = sound.Output;
            source.loop = sound.Loop;
            source.pitch = sound.Pitch;
            source.volume = sound.Volume;
        }

        private AudioSource FindFreeAudiosource() {
            foreach(AudioSource audioSource in audioSources) {
                if(!audioSource.isPlaying) {
                    return audioSource;
                }
            }
            audioSources.Add(gameObject.AddComponent<AudioSource>());
            return audioSources.Last();
        }

        private List<AudioSource> audioSources = new List<AudioSource>();
    }
}
