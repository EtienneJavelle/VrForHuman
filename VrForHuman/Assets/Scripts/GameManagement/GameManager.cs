using CardiacMassage;
using RockVR.Video;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Managers/Game Manager")]
public class GameManager : Etienne.Singleton<GameManager> {

    #region Properties

    public bool replayVideoIsPlaying { get; set; }

    public bool toDebriefScene { get; set; }

    public bool IsArcadeMode { get; set; }
    public bool arrestCardiacStarted { get; set; }

    public PlayerCanvasManager PlayerCanvasManager { get; protected set; }
    public RecordManager RecordManager { get; protected set; }

    public CountTimer CountTimer { get; protected set; }
    public TimingHandeler TimingHandeler { get; protected set; }
    public ScoreManager ScoreManager { get; protected set; }
    public CardiacMassage.CardiacMassage CardiacMassage { get; protected set; }
    public CardiacMassageSaving CardiacMassageSaving => cardiacMassageSaving;
    #endregion

    #region UnityInspector
    [SerializeField] private GameObject levelLoader;

    [SerializeField] private CardiacMassageSaving cardiacMassageSaving;
    #endregion

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

    public void SetPlayerCanvasManager(PlayerCanvasManager _playerCanvasManager) {
        PlayerCanvasManager = _playerCanvasManager;
    }

    public void SetRecordManager(RecordManager _recordManager) {
        RecordManager = _recordManager;
    }

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
        cardiacMassageSaving.totalScore = _score;

        if(CountTimer != null) {
            for(int i = 0; i < CountTimer.timerSteps.Length; i++) {
                if(cardiacMassageSaving.maximumTimeReached >= CountTimer.timerSteps[i].RythmTimeReached) {
                    cardiacMassageSaving.totalScore = Mathf.RoundToInt(cardiacMassageSaving.totalScore * CountTimer.timerSteps[i].MultiplierScore);
                    return;
                }
            }
        }
    }

    public void SetMaximumTimeReached(float _time) {
        cardiacMassageSaving.maximumTimeReached = _time;
    }

    public void SetTimingRanks(Rank[] _timingRanks) {
        cardiacMassageSaving.timingRanks = _timingRanks;
    }

    public void SetDepthRanks(Rank[] _depthRanks) {
        cardiacMassageSaving.depthRanks = _depthRanks;
    }

    public void SetPushsDatas(List<CardiacMassagePressureData> _pushData) {
        cardiacMassageSaving.pushDatas = _pushData;
    }
    #endregion

    private void Update() {
        if(toDebriefScene && VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH) {
            toDebriefScene = false;
            if(RecordManager != null) {
                RecordManager.SetRootFolderVideo();
            }
            DebriefScreen();
        }
    }

    public void EndSimulation() {
        if(PlayerCanvasManager != null) {
            PlayerCanvasManager.ActiveEndSimlulationDisplay(true);
        } else {
            Debug.LogWarning("Not PlayerCanvasManager Found");
        }

        if(RecordManager != null) {
            RecordManager.StopRecord();
        } else {
            Debug.LogWarning("Not RecordManager Found");
        }

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
                SetPushsDatas(CardiacMassage.pushDatas);
                CardiacMassage.gameObject.SetActive(false);
            } else {
                Debug.LogWarning($"No CardiacMassage referenced", this);
            }
        }
    }

    public void DebriefScreen() {
        if(IsArcadeMode) {
            SceneLoader.Instance.ChangeScene(Scenes.DebriefSceneArcadeMode);
        } else {
            //SceneLoader.Instance.ChangeScene(Scenes.DebriefSceneClassicMode);
        }
    }

    #endregion
}
