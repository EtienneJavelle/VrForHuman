using TMPro;
using UnityEngine;

public class CountTimer : MonoBehaviour {
    #region Fields

    private TextMeshProUGUI countText;

    private bool inRythm;

    private float countTime;
    private float maxcountTimeReached;
    #endregion

    #region Properties


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


    public float GetMaxCountTimeReached() {
        return maxcountTimeReached;
    }


    public void InitCountTime() {
        if(countTime > maxcountTimeReached) {
            maxcountTimeReached = countTime;
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
        //this.countText.text = (int)(countSeconds / 60) + "mn " + (int)countSeconds + "s " + (Mathf.Round((countSeconds % 1.0f) * 100));
        countText.text = (int)(countTime / 60) + ":" + (int)countTime;
    }

    #endregion
}
