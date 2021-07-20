using System.Collections;
using UnityEngine;

namespace CardiacMassage {
    public class TimingHandeler : MonoBehaviour {
        [SerializeField] private Rank[] ranks;
        [SerializeField] private float beat;
        [SerializeField] private CardiacMassage cardiacMassage;
        [SerializeField] private AudioClip[] TESTBeatClips;
        private CardiacMassagePressureData lastPressure;
        [SerializeField] private AudioSource TESTAudio;

        private CountTimer countTimer;

        private void Awake() {
            cardiacMassage ??= GetComponent<CardiacMassage>();
            countTimer = FindObjectOfType<CountTimer>();

            cardiacMassage.OnPressureDone += pressure => CalcutaleTimingAccuracy(pressure);
            cardiacMassage.OnMassageStart += () => beatCoroutine = StartCoroutine(Beat());
            cardiacMassage.OnMassageStop += () => StopCoroutine(beatCoroutine);
            if(countTimer != null) {
                cardiacMassage.OnMassageStop += () => countTimer.SetInRythmValue(false);
            }

            TESTAudio ??= GetComponent<AudioSource>();
            if(TESTAudio == null) {
                TESTAudio = gameObject.AddComponent<AudioSource>();
            }
            TESTAudio.playOnAwake = false;

            GameManager.Instance.SetTimingHandeler(this);

        }

        private void Start() {
            GameManager.Instance.SetTimingRanks(GetRanks());
        }

        public Rank[] GetRanks() {
            return ranks;
        }

        private IEnumerator Beat() {
            while(true) {
                yield return new WaitForSeconds(beat / 1000f);
                TESTAudio.PlayOneShot(TESTBeatClips[Random.Range(0, TESTBeatClips.Length)]);
            }
        }


        private CardiacMassagePressureData CalcutaleTimingAccuracy(CardiacMassagePressureData pressure) {
            float time = (pressure.Time - lastPressure.Time) * 1000;
            int rank = ranks.Length - 1;
            for(int i = 0; i < ranks.Length; i++) {
                if(Mathf.Abs(time - beat) <= ranks[i].Offset) {
                    rank = i;
                    break;
                }
            }
            Debug.Log(ranks[rank].Text);


            if(countTimer != null) {
                if(rank <= 1) {
                    countTimer.SetInRythmValue(true);
                } else {
                    countTimer.SetInRythmValue(false);
                }
            }


            ScoreManager _scoreManager = FindObjectOfType<ScoreManager>();
            if(_scoreManager != null) {
                _scoreManager.SetScoreModifier(ranks[rank].Points);

                if(_scoreManager.timeSuccessTextPointSpawns.Count >= 4) {
                    _scoreManager.SetSuccessText(_scoreManager.RandomGenerationSpawners(_scoreManager.timeSuccessTextPointSpawns),
                        ranks[rank].Text, ranks[rank].Colors);
                    ranks[rank].Iterations++;
                }
            }

            lastPressure = pressure;
            return pressure;
        }

        private Coroutine beatCoroutine;
    }
}