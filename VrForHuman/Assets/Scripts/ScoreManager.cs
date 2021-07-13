using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    #region Properties

    private int score;
    private TextMeshProUGUI scoreAmountText;

    private Transform scorePointAmountSpawn;

    private Vector3 minSize;

    #endregion

    #region Unity Inspector

    public UIPointsAmount uIPointsAmount;

    [Space]

    public Vector3 maxSize;
    public float extendSizeDuration;

    #endregion

    #region Behaviour

    #region Initialize

    // Start is called before the first frame update
    void Awake()
    {
        scoreAmountText = GetComponentInChildren<TextMeshProUGUI>();
        scorePointAmountSpawn = GameObject.FindGameObjectWithTag("ScorePointAmountSpawn").transform;

        SetScore();

        minSize = scoreAmountText.transform.localScale;
    }

    #endregion

    private void Update()
    {
        if(scoreAmountText.transform.localScale.x >= maxSize.x && scoreAmountText.transform.localScale.y >= maxSize.y && scoreAmountText.transform.localScale.z >= maxSize.z)
        {
            scoreAmountText.transform.DOScale(minSize, extendSizeDuration);
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeScore(1000);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeScore(-1000);
        }
#endif
    }

    public void ChangeScore(int _amount)
    {
        this.score += _amount;

        if (scorePointAmountSpawn != null)
        {
            GameObject _uiPointsAmount = Instantiate(uIPointsAmount.gameObject);
            _uiPointsAmount.transform.SetParent(scorePointAmountSpawn);
            _uiPointsAmount.transform.localPosition = Vector3.zero;
            _uiPointsAmount.transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("Not ScorePointAmountSpawn founded");
        }

        if(this.score < 0)
        {
            this.score = 0;
        }

        if(this.score > 999999999)
        {
            this.score = 999999999;
        }

        SetScore();
        
    }

    public void SetScore()
    {
        if (scoreAmountText != null)
        {
            scoreAmountText.text = this.score.ToString();

            scoreAmountText.transform.DOScale(maxSize, extendSizeDuration);
        }
        else
        {
            Debug.LogWarning("Not ScoreAmountText founded !");
        }
    }

    #endregion
}
