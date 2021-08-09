using Etienne;
using System.Collections;
using UnityEngine;

namespace CardiacMassage {
    [Requirement(typeof(AudioManager))]
    public class TimingHandeler : MonoBehaviourWithRequirement {
        public Rank[] Ranks => ranks;

        [SerializeField] private Rank[] ranks;
        [SerializeField] private float beat;
        [SerializeField] private CardiacMassage cardiacMassage;
        [SerializeField] private Cue beatCue = new Cue(null);

        private void Awake() {
            cardiacMassage ??= GetComponent<CardiacMassage>();
            countTimer = FindObjectOfType<CountTimer>();

            cardiacMassage.OnPressureDone += pressure => CalcutaleTimingAccuracy(pressure);
            cardiacMassage.OnMassageStart += () => beatCoroutine = StartCoroutine(Beat());
            cardiacMassage.OnMassageStop += () => StopCoroutine(beatCoroutine);
            if(countTimer != null) {
                cardiacMassage.OnMassageStop += () => countTimer.SetInRythmValue(false);
            }
            GameManager.Instance.SetTimingHandeler(this);
        }

        private void Start() {
            GameManager.Instance.SetTimingRanks(Ranks);
        }

        private IEnumerator Beat() {
            while(true) {
                yield return new WaitForSeconds(beat / 1000f);
                AudioManager.Play(beatCue);
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

            if(GameManager.Instance.IsArcadeMode) {
                if(countTimer != null) {
                    if(rank <= 1) {
                        countTimer.SetInRythmValue(true);
                    } else {
                        countTimer.SetInRythmValue(false);
                    }
                } else {
                    Debug.LogWarning("CountTimer not found");
                }
            }

            if(GameManager.Instance.IsArcadeMode) {
                ScoreManager _scoreManager = FindObjectOfType<ScoreManager>();
                if(_scoreManager != null) {
                    _scoreManager.SetScoreModifier(ranks[rank].Points);

                    if(_scoreManager.TimeSuccessTextPointSpawns.Length >= 4) {
                        _scoreManager.SetSuccessText(_scoreManager.RandomGenerationSpawners(_scoreManager.TimeSuccessTextPointSpawns),
                            ranks[rank].Text, ranks[rank].Colors);
                        ranks[rank].Iterations++;
                    }
                } else {
                    Debug.LogWarning("ScoreManager not found");
                }
            }

            lastPressure = pressure;
            return pressure;
        }

        private Coroutine beatCoroutine;
        private CardiacMassagePressureData lastPressure;
        private CountTimer countTimer;
    }
}