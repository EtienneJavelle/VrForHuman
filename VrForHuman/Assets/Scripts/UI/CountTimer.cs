using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountTimer : MonoBehaviour
{
    #region Fields

    private TextMeshProUGUI countText;

    private bool inRythm;
    private float countTime;

    #endregion

    #region Properties


    #endregion

    #region Behaviour

    private void Awake()
    {
        countText = GetComponentInChildren<TextMeshProUGUI>();

        UpdateCountText();
    }

    private void Update()
    {
        if(inRythm)
        {
            AddCountTime(Time.deltaTime);
        }
        else
        {
            InitCountTime();
        }
    }

    public void InitCountTime()
    {
        this.countTime = 0;
        UpdateCountText();
    }

    private void AddCountTime(float _amount)
    {
        this.countTime += _amount;

        UpdateCountText();
    }

    public void SetInRythmValue(bool _value)
    {
        this.inRythm = _value;
    }

    private void UpdateCountText()
    {
        //this.countText.text = (int)(countSeconds / 60) + "mn " + (int)countSeconds + "s " + (Mathf.Round((countSeconds % 1.0f) * 100));
        this.countText.text = (int)(countTime / 60) + ":" + (int)countTime;
    }

    #endregion
}
