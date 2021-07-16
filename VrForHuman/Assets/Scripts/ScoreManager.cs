using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardiacMassage {
    public class ScoreManager : MonoBehaviour {
        #region Fields

        private CardiacMassage cardiacMassage;

        private int score;
        private TextMeshProUGUI scoreAmountText;

        private List<Transform> scorePointAmountSpawns = new List<Transform>();
        private Transform successTextPointSpawn;

        private Vector3 minSize;

        #endregion

        #region Unity Inspector

        [SerializeField] private UITextDisplay uiTextDisplay;

        [Space]

        [SerializeField] private DepthRank[] depthRanks;

        [Space]

        [SerializeField] private Vector3 maxSizeUp, maxSizeDown;
        [SerializeField] private float extendSizeDuration;

        #endregion

        #region Behaviour

        #region Initialize

        // Start is called before the first frame update
        private void Awake() {
            cardiacMassage = FindObjectOfType<CardiacMassage>();

            scoreAmountText = GetComponentInChildren<TextMeshProUGUI>();

            GameObject[] _scoreUiSpawnPoints = GameObject.FindGameObjectsWithTag("ScorePointAmountSpawn");
            for(int i = 0; i < _scoreUiSpawnPoints.Length; i++) {
                scorePointAmountSpawns.Add(_scoreUiSpawnPoints[i].transform);
            }

            successTextPointSpawn = GameObject.FindGameObjectWithTag("SuccessTextPointSpawn").transform;


            SetScore(0);

            minSize = scoreAmountText.transform.localScale;

            if(cardiacMassage != null) {
                cardiacMassage.OnPressureBegin += () => Debug.Log("TestBegin");

                cardiacMassage.OnPressureDone += _ => Debug.Log("TestPressure");
                cardiacMassage.OnPressureDone += pushData => CalculateScoreValue(pushData);
            }
        }

        #endregion

        private void Update() {
            if((scoreAmountText.transform.localScale.x >= maxSizeUp.x && scoreAmountText.transform.localScale.y >= maxSizeUp.y
                && scoreAmountText.transform.localScale.z >= maxSizeUp.z) || (scoreAmountText.transform.localScale.x <= maxSizeDown.x
                && scoreAmountText.transform.localScale.y <= maxSizeDown.y && scoreAmountText.transform.localScale.z <= maxSizeDown.z)) {
                scoreAmountText.transform.DOScale(minSize, extendSizeDuration);
            }



#if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.I)) {
                ChangeScore(1000);
            }

            if(Input.GetKeyDown(KeyCode.K)) {
                ChangeScore(-1000);
            }
#endif
        }

        private void CalculateScoreValue(CardiacMassagePressureData _pushData) {
            Debug.Log("PushData :" + (Mathf.Abs(_pushData.Depth)));
            float _scoreValue = 1.0f;
            if((Mathf.Abs(_pushData.Depth)) < 0.5f) {
                _scoreValue = (Mathf.Abs(_pushData.Depth)) * 1000;
            } else {
                _scoreValue = 1000;
            }
            ChangeScore((int)_scoreValue);

            for(int i = 0; i < depthRanks.Length; i++) {
                if((Mathf.Abs(_pushData.Depth)) >= depthRanks[i].Value) {
                    SetSuccessText(depthRanks[i].Text, depthRanks[i].Colors);
                    return;
                }
            }
            SetSuccessText(depthRanks[depthRanks.Length - 1].Text, depthRanks[depthRanks.Length - 1].Colors);
        }

        public void ChangeScore(int _amount) {
            score += _amount;

            if(scorePointAmountSpawns.Count >= 4) {
                int aleat = Random.Range(1, 101);
                Transform scorePointAmountSpawn = scorePointAmountSpawns[0];
                if(aleat >= 25 && aleat < 50) {
                    scorePointAmountSpawn = scorePointAmountSpawns[1];
                } else if(aleat >= 50 && aleat < 75) {
                    scorePointAmountSpawn = scorePointAmountSpawns[2];
                } else if(aleat >= 75) {
                    scorePointAmountSpawn = scorePointAmountSpawns[3];
                }
                UITextDisplay _uiTextDisplay = Instantiate(uiTextDisplay);
                _uiTextDisplay.transform.SetParent(scorePointAmountSpawn);
                _uiTextDisplay.transform.localPosition = Vector3.zero;
                _uiTextDisplay.transform.rotation = Quaternion.identity;
                _uiTextDisplay.SetPoints(_amount);
            } else {
                Debug.LogWarning("Not ScorePointAmountSpawn founded");
            }

            if(score < 0) {
                score = 0;
            }

            if(score > 999999999) {
                score = 999999999;
            }

            SetScore(_amount);

        }

        public void SetScore(int _amount) {
            if(scoreAmountText != null) {
                scoreAmountText.text = score.ToString();

                if(_amount >= 0) {
                    scoreAmountText.transform.DOScale(maxSizeUp, extendSizeDuration);
                } else {
                    scoreAmountText.transform.DOScale(maxSizeDown, extendSizeDuration);
                }
            } else {
                Debug.LogWarning("Not ScoreAmountText founded !");
            }
        }

        private void SetSuccessText(string _text, VertexGradient _colors) {
            if(successTextPointSpawn != null) {

                UITextDisplay _uiTextDisplay = Instantiate(uiTextDisplay);
                _uiTextDisplay.transform.SetParent(successTextPointSpawn);
                _uiTextDisplay.transform.localPosition = Vector3.zero;
                _uiTextDisplay.transform.rotation = Quaternion.identity;

                _uiTextDisplay.SetText(_text, _colors);


            } else {
                Debug.LogWarning("Not SuccessTextPointSpawn founded");
            }
        }

        #endregion
    }
}
