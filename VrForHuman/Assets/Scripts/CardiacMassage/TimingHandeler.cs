using System.Collections;
using UnityEngine;
using TMPro;

namespace CardiacMassage {
    public class TimingHandeler : MonoBehaviour {
        [SerializeField] private Rank[] ranks;
        [SerializeField] private VertexGradient[] ranksColors;
        [SerializeField] private float beat;
        [SerializeField] private CardiacMassage cardiacMassage;
        [SerializeField] private AudioClip[] TESTBeatClips;
        private CardiacMassagePressureData lastPressure;
        [SerializeField] private AudioSource TESTAudio;
        private void Awake() {
            cardiacMassage ??= GetComponent<CardiacMassage>();
            cardiacMassage.OnPressureDone += pressure => CalcutaleTimingAccuracy(pressure);
            cardiacMassage.OnMassageStart += () => beatCoroutine = StartCoroutine(Beat());
            cardiacMassage.OnMassageStop += () => StopCoroutine(beatCoroutine);


            TESTAudio ??= GetComponent<AudioSource>();
            if(TESTAudio == null) {
                TESTAudio = gameObject.AddComponent<AudioSource>();
            }
            TESTAudio.playOnAwake = false;

        }

        private IEnumerator Beat() {
            while(true) {
                yield return new WaitForSeconds(beat / 1000f);
                TESTAudio.PlayOneShot(TESTBeatClips[Random.Range(0, TESTBeatClips.Length)]);
            }
        }


        private CardiacMassagePressureData CalcutaleTimingAccuracy(CardiacMassagePressureData pressure) {
            float time = (pressure.Time - lastPressure.Time) * 1000;
            print(time - beat);
            int rank = ranks.Length - 1;
            for(int i = 0; i < ranks.Length; i++) {
                if(Mathf.Abs(time - beat) <= ranks[i].Offset) {
                    rank = i;
                    break;
                }
            }
            Debug.Log(ranks[rank].DisplayName);

            ScoreManager _scoreManager = FindObjectOfType<ScoreManager>();
            if(_scoreManager != null)
            {
                _scoreManager.SetScoreModifier(ranks[rank].Points);
                _scoreManager.SetSuccessText(_scoreManager.timeSuccessTextPointSpawn, ranks[rank].DisplayName, ranksColors[rank]);
            }

            lastPressure = pressure;
            return pressure;
        }

        private void Update() {

        }

        private Coroutine beatCoroutine;
    }
}