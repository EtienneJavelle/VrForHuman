using System.Collections.Generic;
using CardiacMassage;
using UnityEngine;

[AddComponentMenu("Managers/Game Manager")]
public class GameManager : Etienne.Singleton<GameManager> {

    #region Properties
    public bool IsArcadeMode { get; set; }
    public CountTimer countTimer { get; protected set; }
    public TimingHandeler timingHandeler { get; protected set; }
    public ScoreManager scoreManager { get; protected set; }
    public CardiacMassage.CardiacMassage cardiacMassage { get; protected set; }
    #endregion

    #region UnityInspector
    //todo soity faire un getter soit ne pas la mettre public
    public GameObject levelLoader;

    #endregion

    private CardiacMassageSavingData cardiacMassageSavingData;

    #region Behaviour

    //todo getter
    public CardiacMassageSavingData GetCardiacMassageSavingData() {
        return cardiacMassageSavingData;
    }

    #region Initialize
    public void Start() {
        EssentialLoading();
    }

    //todo metrtre les stters au meme endroit (regiuon?)
    public void SetCountTimer(CountTimer _countTimer) {
        countTimer = _countTimer;
    }
    public void SetTimingHandeler(TimingHandeler _timingHandeler) {
        timingHandeler = _timingHandeler;
    }

    public void SetScoreManager(ScoreManager _scoreManager) {
        scoreManager = _scoreManager;
    }

    public void SetCardiacMassage(CardiacMassage.CardiacMassage _cardiacMassage) {
        cardiacMassage = _cardiacMassage;
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


    public void EndGame() {
        if(IsArcadeMode) {
            //todo Specifier les erreurs
            if(countTimer != null) {
                SetMaximumTimeReached(countTimer.GetMaxCountTimeReached());
            } else {
                Debug.LogError("No Count Timer");
            }

            if(timingHandeler != null) {
                SetTimingRanks(timingHandeler.GetRanks());
            }

            if(scoreManager != null) {
                SetDepthRanks(scoreManager.GetRanks());
                SetTotalScore(scoreManager.GetScore());
            }

            if(cardiacMassage != null) {
                //todo SetPushDatas(pushData à récup sur Cardiac Massage)
            }

            SceneLoader.Instance.ChangeScene(Scenes.EndGame);
        }
    }
    #endregion
}
