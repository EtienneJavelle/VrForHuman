using CardiacMassage;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Managers/Game Manager")]
public class GameManager : Etienne.Singleton<GameManager> {

    #region Properties
    public bool IsArcadeMode { get; set; }
    public CountTimer CountTimer { get; protected set; }
    public TimingHandeler TimingHandeler { get; protected set; }
    public ScoreManager ScoreManager { get; protected set; }
    public CardiacMassage.CardiacMassage CardiacMassage { get; protected set; }
    public CardiacMassageSavingData CardiacMassageSavingData => cardiacMassageSavingData;
    #endregion

    #region UnityInspector
    [SerializeField] private GameObject levelLoader;
    #endregion

    private CardiacMassageSavingData cardiacMassageSavingData;

    #region Behaviour

    #region Initialize
    public void Start() {
        EssentialLoading();
    }

    private void EssentialLoading() {
        if(levelLoader != null && SceneLoader.Instance == null) {
            Instantiate(levelLoader);
        }
    }

    #endregion

    #region Setters
    public void SetCountTimer(CountTimer _countTimer) {
        CountTimer = _countTimer;
    }

    public void SetTimingHandeler(TimingHandeler _timingHandeler) {
        TimingHandeler = _timingHandeler;
    }

    public void SetScoreManager(ScoreManager _scoreManager) {
        ScoreManager = _scoreManager;
    }

    public void SetCardiacMassage(CardiacMassage.CardiacMassage _cardiacMassage) {
        CardiacMassage = _cardiacMassage;
    }

    public void SetTotalScore(int _score) {
        cardiacMassageSavingData.totalScore = _score;

        if(CountTimer != null) {
            for(int i = 0; i < CountTimer.timerSteps.Length; i++) {
                if(cardiacMassageSavingData.maximumTimeReached >= CountTimer.timerSteps[i].RythmTimeReached) {
                    cardiacMassageSavingData.totalScore = Mathf.RoundToInt(cardiacMassageSavingData.totalScore * CountTimer.timerSteps[i].MultiplierScore);
                    return;
                }
            }
        }
    }

    public void SetMaximumTimeReached(float _time) {
        cardiacMassageSavingData.maximumTimeReached = _time;
    }

    public void SetTimingRanks(Rank[] _timingRanks) {
        cardiacMassageSavingData.timingRanks = _timingRanks;
    }

    public void SetDepthRanks(Rank[] _depthRanks) {
        cardiacMassageSavingData.depthRanks = _depthRanks;
    }

    public void SetPushsDatas(List<CardiacMassagePressureData> _pushData) {
        cardiacMassageSavingData.pushDatas = _pushData;
    }
    #endregion

    public void EndGame() {
        if(IsArcadeMode) {
            if(CountTimer != null) {
                SetMaximumTimeReached(CountTimer.MaxcountTimeReached);
            } else {
                Debug.LogWarning($"No CountTimer referenced", this);
            }

            if(TimingHandeler != null) {
                SetTimingRanks(TimingHandeler.Ranks);
            } else {
                Debug.LogWarning($"No TimingHandeler referenced", this);
            }

            if(ScoreManager != null) {
                SetDepthRanks(ScoreManager.Ranks);
                SetTotalScore(ScoreManager.Score);
            } else {
                Debug.LogWarning($"No ScoreManager referenced", this);
            }

            if(CardiacMassage != null) {
                //todo Yanis SetPushDatas(pushData à récup sur Cardiac Massage)
            } else {
                Debug.LogWarning($"No CardiacMassage referenced", this);
            }

            SceneLoader.Instance.ChangeScene(Scenes.EndGame);
        }
    }

    #endregion
}
