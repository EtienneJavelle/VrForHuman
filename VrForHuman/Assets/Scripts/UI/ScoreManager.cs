using System.Collections.Generic;
using DG.Tweening;
using Etienne;
using TMPro;
using UnityEngine;

namespace CardiacMassage {

    [Requirement(typeof(GameManager))]
    public class ScoreManager : MonoBehaviourWithRequirement {
        #region Properties

        public List<SpawnJitter> TimeSuccessTextPointSpawns => timeSuccessTextPointSpawns;

        #endregion

        #region Unity Inspector

        [SerializeField] private UITextDisplay uiTextDisplay;

        [Space]

        [SerializeField] private Rank[] ranks;

        [Space]

        [SerializeField] private Vector3 maxSizeUp, maxSizeDown;
        [SerializeField] private float extendSizeDuration;

        public List<SpawnJitter> timeSuccessTextPointSpawns = new List<SpawnJitter>();
        #endregion

        #region Fields

        private CardiacMassage cardiacMassage;

        private int score;
        private float scoreModifier;
        private TextMeshProUGUI scoreAmountText;

        private List<SpawnJitter> scorePointAmountSpawns = new List<SpawnJitter>();
        private List<SpawnJitter> depthSuccessTextPointSpawns = new List<SpawnJitter>();

        private Vector3 minSize;

        #endregion

        #region Behaviour

        #region Initialize

        private void Awake() {
            if(GameManager.Instance.IsArcadeMode == false) {
                transform.parent.gameObject.SetActive(false);
                return;
            }

            cardiacMassage = FindObjectOfType<CardiacMassage>();

            scoreAmountText = GetComponentInChildren<TextMeshProUGUI>();

            AnimateScore(0);

            minSize = scoreAmountText.transform.localScale;

            if(cardiacMassage != null) {
                cardiacMassage.OnPressureDone += pushData => CalculateScoreValue(pushData);
            }

            GameManager.Instance.SetScoreManager(this);
        }

        private void Start() {
            GameManager.Instance.SetDepthRanks(GetRanks());
            GetJitters();
        }

        [ContextMenu("GetJitters")]
        private void GetJitters() {
            //todo : deriver de la classe jitter pour les trois differents typoews
            depthSuccessTextPointSpawns.Clear();
            timeSuccessTextPointSpawns.Clear();
            scorePointAmountSpawns.Clear();
            SpawnJitter[] jpawnJitters = transform.parent.GetComponentsInChildren<SpawnJitter>();
            for(int i = 0; i < jpawnJitters.Length; i++) {
                for(int u = 0; u < 4; u++) {
                    if(i == 0) {
                        depthSuccessTextPointSpawns.Add(jpawnJitters[u + i]);
                    } else if(i == 1) {
                        timeSuccessTextPointSpawns.Add(jpawnJitters[u + i]);
                    } else if(i == 2) {
                        scorePointAmountSpawns.Add(jpawnJitters[u + i]);
                    }
                }
            }
        }

        #endregion


        //todo : public int Score => score;
        public int GetScore() {
            return score;
        }

        public Rank[] GetRanks() {
            return ranks;
        }


        public SpawnJitter RandomGenerationSpawners(List<SpawnJitter> _spawnPoints) {
            int random = Random.Range(1, 4);
            return _spawnPoints[random];
        }

        private void CalculateScoreValue(CardiacMassagePressureData _pushData) {
            for(int i = 0; i < ranks.Length; i++) {
                if((Mathf.Abs(_pushData.Depth)) >= ranks[i].Offset) {
                    ChangeScore(ranks[i].Points + (int)scoreModifier);

                    if(depthSuccessTextPointSpawns.Count >= 4) {
                        SetSuccessText(RandomGenerationSpawners(depthSuccessTextPointSpawns), ranks[i].Text, ranks[i].Colors);
                        ranks[i].Iterations++;
                    }

                    return;
                }
            }
            if(depthSuccessTextPointSpawns.Count >= 4) {
                SetSuccessText(RandomGenerationSpawners(depthSuccessTextPointSpawns), ranks[ranks.Length - 1].Text,
                    ranks[ranks.Length - 1].Colors);
                ranks[ranks.Length - 1].Iterations++;
            }
        }

        public void SetScoreModifier(float _amount) {
            scoreModifier = _amount;
        }

        public void ChangeScore(int _amount) {
            score = Mathf.Clamp(score + _amount, 0, 999999999);
            //todo REset Scoremodifier
            SetScoreModifier(0.0f);

            if(scorePointAmountSpawns.Count >= 4) {
                SpawnJitter parent = RandomGenerationSpawners(scorePointAmountSpawns);
                UITextDisplay _uiTextDisplay = InstantiateUITextDisplay(parent);
                _uiTextDisplay.SetPoints(_amount);
            } else {
                Debug.LogWarning($"No ScorePointAmountSpawn referenced", this);
            }
            AnimateScore(_amount);
        }

        public void AnimateScore(int _amount) {
            if(scoreAmountText != null) {
                scoreAmountText.text = score.ToString();
                scoreAmountText.transform.DOComplete();
                scoreAmountText.transform.DOShakeRotation(extendSizeDuration, 10);
                if(_amount >= 0) {
                    scoreAmountText.transform.DOPunchScale(maxSizeUp, extendSizeDuration);
                } else {
                    scoreAmountText.transform.DOPunchScale(maxSizeDown, extendSizeDuration);
                }
            } else {
                Debug.LogWarning($"No ScoreAmountText referenced", this);
            }
        }

        public void SetSuccessText(SpawnJitter _spawnPoint, string _text, VertexGradient _colors) {
            if(_spawnPoint != null) {
                UITextDisplay _uiTextDisplay = InstantiateUITextDisplay(_spawnPoint);
                _uiTextDisplay.SetText(_text, _colors);
            } else {
                Debug.LogWarning($"No SuccessTextPointSpawn referenced", this);
            }
        }

        private UITextDisplay InstantiateUITextDisplay(SpawnJitter parent) {
            UITextDisplay _uiTextDisplay = Instantiate(uiTextDisplay, parent.transform, true);
            _uiTextDisplay.transform.localScale = Vector3.one;
            _uiTextDisplay.GetComponent<RectTransform>().localPosition = Random.insideUnitCircle * parent.Range;
            return _uiTextDisplay;
        }

        #endregion
    }
}
