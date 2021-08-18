using DG.Tweening;
using Etienne;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace CardiacMassage {
    [Etienne.Requirement(typeof(AudioManager))]
    public class Defibrilation : Etienne.MonoBehaviourWithRequirement {
        [SerializeField] private TMPro.TextMeshProUGUI message;
        [SerializeField] private Interactable cardiacMassage;
        [SerializeField] private Etienne.SoundParameters soundParameters;

        [Header("Audio Clips")]
        [SerializeField]
        private AudioClip placeElectrodesClip;
        [SerializeField]
        private AudioClip
            analyseClip,
            chocClip,
            chocDeliveredClip,
            ambulanceClip,
            tutoClip,
            metronomeClip;

        private bool isStarted, isRunning;
        private AudioSource audioSource;
        private Coroutine coroutine;
        private Lid lid;

        private void Awake() {
            cardiacMassage ??= FindObjectOfType<CardiacMassage>().GetComponent<Interactable>();
            message ??= GetComponentInChildren<TMPro.TextMeshProUGUI>();
            UpdateMessage("");
        }

        public void SetLid(Lid lid) {
            lid.OnOpenLid += PlayPlaceElectrodesSound;
            this.lid = lid;
        }

        private void PlayPlaceElectrodesSound() {
            lid.OnOpenLid -= PlayPlaceElectrodesSound;
            UpdateMessage("Placez les electrodes");
            audioSource = AudioManager.Play(new Sound(placeElectrodesClip, soundParameters));
        }

        private void Update() {
            if(isRunning) {
                if(cardiacMassage.isHovering) {
                    GameManager.Instance.PlayerCanvasManager.BeHurt();
                }
            }
        }

        public bool StartDefibrilation() {
            if(isStarted) return false;
            isStarted = true;
            coroutine = StartCoroutine(DefibrilationCoroutine());
            return true;
        }

        public IEnumerator DefibrilationCoroutine() {
            if(audioSource.isPlaying && audioSource.clip == placeElectrodesClip) {
                audioSource.Stop();
            }
            yield return DefibrilationMessageAndWait(analyseClip, "Analyse, ne pas toucher le patient");
            yield return DefibrilationMessageAndWait(chocClip, "Choc recommandé, <b>écartez vous du patient</b>");
            isRunning = true;
            yield return DefibrilationMessageAndWait(chocDeliveredClip, "Choc délivré");
            isRunning = false;
            yield return DefibrilationMessageAndWait(ambulanceClip, "Assurez vous qu'une ambulance\r\nà bien été apellé.");
            yield return DefibrilationMessageAndWait(tutoClip, "Il n'y a plus de risque de toucher le patient,\r\nPlacez vous bien pour commencer le massage");
            UpdateMessage("Massez au rythme du bip");
            audioSource = AudioManager.Play(new Sound(metronomeClip,
                new SoundParameters(soundParameters.Volume, soundParameters.Pitch, true, soundParameters.SpacialBlend)),
               transform);
        }

        private WaitForSeconds DefibrilationMessageAndWait(AudioClip clip, string message) {
            UpdateMessage(message);
            audioSource = AudioManager.Play(new Sound(clip, soundParameters), transform);
            return new WaitForSeconds(clip.length);
        }

        public void UpdateMessage(string text) {
            message.text = text;
            message.DOComplete();
            message.transform.DOPunchScale(Vector2.one * 1.25f, .25f);
            message.transform.DOShakeRotation(.25f, 10);
        }

        public void PauseDefibrilation() {
            if(!isStarted) return;
            isStarted = false;
            StopCoroutine(coroutine);
            UpdateMessage("Un ou plusieurs electrode(s) on été décollé(s).\r\nVeuillez les recoller.");
        }
    }
}