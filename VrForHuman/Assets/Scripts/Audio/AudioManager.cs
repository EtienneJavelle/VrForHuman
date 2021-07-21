using System.Collections.Generic;
using Etienne;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Audio/Audio Managers/Audio Manager")]
public class AudioManager : Singleton<AudioManager> {
    [SerializeField] private AudioMixerGroup output;
    protected override void Awake() {
        base.Awake();

        audioSources.Add(gameObject.AddComponent<AudioSource>());
    }

    public static void Play(Sound sound) {
        AudioSource source = Instance.FindFreeAudiosource(Instance.audioSources);
        source.outputAudioMixerGroup = Instance.output;
        source.SetSoundToSource(sound);
        source.Play();
    }

    private List<AudioSource> audioSources = new List<AudioSource>();
}