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
            AudioSource source = Instance.FindFreeAudiosource(Instance.audioSources);
            source.SetSoundToSource(sound);
            source.Play();
        }

        private List<AudioSource> audioSources = new List<AudioSource>();
    }
}
