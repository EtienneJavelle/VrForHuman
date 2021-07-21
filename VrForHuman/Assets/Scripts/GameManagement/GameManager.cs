using CardiacMassage;
using UnityEngine;

[AddComponentMenu("Managers/Game Manager")]
public class GameManager : Etienne.Singleton<GameManager> {

    #region Properties
    public bool IsArcadeMode { get; set; }


    public CountTimer countTimer { get; protected set; }
    public TimingHandeler timingHandeler { get; protected set; }
    public ScoreManager scoreManager { get; protected set; }


    private CardiacMassageSavingData cardiacMassageSavingData;

    #endregion

    #region UnityInspector

    public GameObject levelLoader;

    #endregion

    #region Behaviour

    public void Start() {
        EssentialLoading();
    }

    public CardiacMassageSavingData GetCardiacMassageSavingData() {
        return cardiacMassageSavingData;
    }

    #region Initialize

    public void SetCountTimer(CountTimer _countTimer) {
        countTimer = _countTimer;
    }
    public void SetTimingHandeler(TimingHandeler _timingHandeler) {
        timingHandeler = _timingHandeler;
    }

    public void SetScoreManager(ScoreManager _scoreManager) {
        scoreManager = _scoreManager;
    }

    private void EssentialLoading() {
        if(levelLoader != null && SceneLoader.Instance == null) {
            Instantiate(levelLoader);
        }
    }

    #endregion

    public void SetTotalScore(int _score) {
        cardiacMassageSavingData.totalScore = _score;

        if(countTimer != null) {
            for(int i = 0; i < countTimer.timerSteps.Length; i++) {
                if(cardiacMassageSavingData.maximumTimeReached >= countTimer.timerSteps[i].RythmTimeReached) {
                    cardiacMassageSavingData.totalScore = Mathf.RoundToInt(cardiacMassageSavingData.totalScore * countTimer.timerSteps[i].MultiplierScore);
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


    public void EndGame() {
        if(IsArcadeMode) {
            if(countTimer != null) {
                SetMaximumTimeReached(countTimer.GetMaxCountTimeReached());
            }

            if(timingHandeler != null) {
                SetTimingRanks(timingHandeler.GetRanks());
            }

            if(scoreManager != null) {
                SetDepthRanks(scoreManager.GetRanks());
                SetTotalScore(scoreManager.GetScore());
            }

            SceneLoader.Instance.ChangeScene(Scenes.EndGame);
        }
    }

    #endregion

}
