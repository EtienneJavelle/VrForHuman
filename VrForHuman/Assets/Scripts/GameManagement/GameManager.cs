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

    #region Behaviour

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

    #endregion

    public void SetTotalScore(int _score) {
        cardiacMassageSavingData.totalScore = _score;
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

    #endregion

}
