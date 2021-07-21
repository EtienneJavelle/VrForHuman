using System.Collections.Generic;
using Etienne;
using UnityEngine;

[AddComponentMenu("Audio/Audio Managers/Audio Manager")]
public class AudioManager : Singleton<AudioManager> {
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