using CardiacMassage;
using TMPro;
using UnityEngine;

public class CountTimer : MonoBehaviour {
    #region Properties

    public float MaxcountTimeReached { get; protected set; }

    #endregion

    #region UnityInspector
    public TimerStep[] timerSteps;

    #endregion

    #region Fields
    private TextMeshProUGUI countText;

    private bool inRythm;

    private float countTime;
    #endregion

    #region Behaviour

    private void Awake() {
        if(GameManager.Instance.IsArcadeMode == false) {
            gameObject.SetActive(false);
            return;
        }

        countText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateCountText();

        GameManager.Instance.SetCountTimer(this);
    }

    private void Update() {
        if(inRythm) {
            AddCountTime(Time.deltaTime);
        } else {
            InitCountTime();
        }
    }

    public void InitCountTime() {
        if(countTime > MaxcountTimeReached) {
            MaxcountTimeReached = countTime;
        }
        countTime = 0;
        UpdateCountText();
    }

    private void AddCountTime(float _amount) {
        countTime += _amount;

        UpdateCountText();
    }

    public void SetInRythmValue(bool _value) {
        inRythm = _value;
    }

    private void UpdateCountText() {
        countText.text = (int)(countTime / 60) + ":" + (int)countTime;
    }
    #endregion
}
