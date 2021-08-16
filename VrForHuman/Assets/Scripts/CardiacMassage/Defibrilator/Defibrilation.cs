using DG.Tweening;
using Etienne;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace CardiacMassage {
    [Etienne.Requirement(typeof(AudioManager))]
    public class Defibrilation : Etienne.MonoBehaviourWithRequirement {
        [SerializeField] private TMPro.TextMeshProUGUI message;
        [SerializeField] private Sound sound;
        [SerializeField] private float intervalBetweenDefibrilations = 20f;
        [SerializeField] private int backOffCountdown = 5, backOffAdditionalTime = 3;
        [SerializeField] private Interactable cardiacMassage;

        private bool isStarted, isRunning;
        private AudioSource audioSource;
        private Coroutine coroutine;

        private void Awake() {
            cardiacMassage ??= FindObjectOfType<CardiacMassage>().GetComponent<Interactable>();
            message ??= GetComponentInChildren<TMPro.TextMeshProUGUI>();
            UpdateMessage("Veuillez coller Les Electrodes.");
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
            UpdateMessage("Massez au rythme du click!");
            if(coroutine != null) {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(DefibrilationInterval());
            return true;
        }

        public IEnumerator DefibrilationInterval() {
            audioSource = AudioManager.Play(sound, transform);
            yield return new WaitForSeconds(intervalBetweenDefibrilations);
            audioSource.Stop();
            coroutine = StartCoroutine(DefibrilationCountdown());
        }

        public IEnumerator DefibrilationCountdown() {
            for(int i = backOffCountdown - 1; i >= 0; i--) {
                UpdateMessage($"Reculez !\r\nDefibrilation dans {i} secondes.");
                yield return new WaitForSeconds(1);
            }
            //todo defibrilation points
            //todo defbrilation degats
            UpdateMessage("Derfibrilation");
            isRunning = true;
            yield return new WaitForSeconds(backOffAdditionalTime);
            UpdateMessage("Reprenez le massage !");
            isRunning = false;
            coroutine = StartCoroutine(DefibrilationInterval());
        }

        private void UpdateMessage(string text) {
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