using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardiacMassage {
    public class ScoreManager : MonoBehaviour {
        #region Fields

        private CardiacMassage cardiacMassage;

        private int score;
        private float scoreModifier;
        private TextMeshProUGUI scoreAmountText;

        private List<Transform> scorePointAmountSpawns = new List<Transform>();
        private List<Transform> depthSuccessTextPointSpawns = new List<Transform>();

        private Vector3 minSize;

        #endregion

        #region Properties

        public List<Transform> timeSuccessTextPointSpawns { get; protected set; }

        #endregion

        #region Unity Inspector

        [SerializeField] private UITextDisplay uiTextDisplay;

        [Space]

        [SerializeField] private Rank[] ranks;

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

            GameObject[] _depthSuccessTextPointsSpawns = GameObject.FindGameObjectsWithTag("DepthSuccessTextPointSpawn");
            for (int i = 0; i < _depthSuccessTextPointsSpawns.Length; i++)
            {
                depthSuccessTextPointSpawns.Add(_depthSuccessTextPointsSpawns[i].transform);
            }

            timeSuccessTextPointSpawns = new List<Transform>();
            GameObject[] _timeSuccessTextPointsSpawns = GameObject.FindGameObjectsWithTag("TimeSuccessTextPointSpawn");
            for (int i = 0; i < _timeSuccessTextPointsSpawns.Length; i++)
            {
                timeSuccessTextPointSpawns.Add(_timeSuccessTextPointsSpawns[i].transform);
            }


            SetScore(0);

            minSize = scoreAmountText.transform.localScale;

            if(cardiacMassage != null) {
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

        public Transform RandomGenerationSpawners(List<Transform> _spawnPoints)
        {
            int aleat = Random.Range(1, 101);
            Transform scorePointAmountSpawn = _spawnPoints[0];
            if (aleat >= 25 && aleat < 50)
            {
                scorePointAmountSpawn = _spawnPoints[1];
            }
            else if (aleat >= 50 && aleat < 75)
            {
                scorePointAmountSpawn = _spawnPoints[2];
            }
            else if (aleat >= 75)
            {
                scorePointAmountSpawn = _spawnPoints[3];
            }

            return scorePointAmountSpawn;
        }

        private void CalculateScoreValue(CardiacMassagePressureData _pushData) {
            for(int i = 0; i < ranks.Length; i++) {
                if((Mathf.Abs(_pushData.Depth)) >= ranks[i].Offset) {
                    ChangeScore(ranks[i].Points + (int)scoreModifier);

                    if (depthSuccessTextPointSpawns.Count >= 4)
                    {
                        SetSuccessText(RandomGenerationSpawners(depthSuccessTextPointSpawns), ranks[i].Text, ranks[i].Colors);
                    }

                    return;
                }
            }
            if (depthSuccessTextPointSpawns.Count >= 4)
            {
                SetSuccessText(RandomGenerationSpawners(depthSuccessTextPointSpawns), ranks[ranks.Length - 1].Text, 
                    ranks[ranks.Length - 1].Colors);
            }
        }

        public void SetScoreModifier(float _amount) {
            scoreModifier = _amount;
        }

        public void ChangeScore(int _amount) {
            score += _amount;
            SetScoreModifier(0.0f);

            if(scorePointAmountSpawns.Count >= 4) {
                
                UITextDisplay _uiTextDisplay = Instantiate(uiTextDisplay);
                _uiTextDisplay.transform.SetParent(RandomGenerationSpawners(scorePointAmountSpawns));
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

        public void SetSuccessText(Transform _spawnPoint, string _text, VertexGradient _colors) {
            if(_spawnPoint != null) {

                UITextDisplay _uiTextDisplay = Instantiate(uiTextDisplay);
                _uiTextDisplay.transform.SetParent(_spawnPoint);
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
