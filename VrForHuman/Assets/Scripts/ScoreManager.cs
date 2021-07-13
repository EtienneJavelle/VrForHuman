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

    private List<Transform> scorePointAmountSpawns = new List<Transform>();

    private Vector3 minSize;
    private bool pointsLost;

    #endregion

    #region Unity Inspector

    public UIPointsAmount uIPointsAmount;

    [Space]

    public Vector3 maxSizeUp, maxSizeDown;
    public float extendSizeDuration;

    #endregion

    #region Behaviour

    #region Initialize

    // Start is called before the first frame update
    void Awake()
    {
        scoreAmountText = GetComponentInChildren<TextMeshProUGUI>();
        var _scoreUiSpawnPoints = GameObject.FindGameObjectsWithTag("ScorePointAmountSpawn");
        for (int i = 0; i < _scoreUiSpawnPoints.Length; i++)
        {
            scorePointAmountSpawns.Add(_scoreUiSpawnPoints[i].transform);
        }

        SetScore(0);

        minSize = scoreAmountText.transform.localScale;
    }

    #endregion

    private void Update()
    {
        if((scoreAmountText.transform.localScale.x >= maxSizeUp.x && scoreAmountText.transform.localScale.y >= maxSizeUp.y 
            && scoreAmountText.transform.localScale.z >= maxSizeUp.z) || (scoreAmountText.transform.localScale.x <= maxSizeDown.x 
            && scoreAmountText.transform.localScale.y <= maxSizeDown.y && scoreAmountText.transform.localScale.z <= maxSizeDown.z))
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

        if (scorePointAmountSpawns.Count >= 4)
        {
            int aleat = Random.Range(1, 101);
            Transform scorePointAmountSpawn = scorePointAmountSpawns[0];
            if (aleat >= 25 && aleat < 50)
            {
                scorePointAmountSpawn = scorePointAmountSpawns[1];
            }
            else if (aleat >= 50 && aleat < 75)
            {
                scorePointAmountSpawn = scorePointAmountSpawns[2];
            }
            else if (aleat >= 75)
            {
                scorePointAmountSpawn = scorePointAmountSpawns[3];
            }
            UIPointsAmount _uiPointsAmount = Instantiate(uIPointsAmount);
            _uiPointsAmount.transform.SetParent(scorePointAmountSpawn);
            _uiPointsAmount.transform.localPosition = Vector3.zero;
            _uiPointsAmount.transform.rotation = Quaternion.identity;
            _uiPointsAmount.SetPoints(_amount);
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

        SetScore(_amount);
        
    }

    public void SetScore(int _amount)
    {
        if (scoreAmountText != null)
        {
            scoreAmountText.text = this.score.ToString();

            if (_amount >= 0)
            {
                scoreAmountText.transform.DOScale(maxSizeUp, extendSizeDuration);
            }
            else
            {
                scoreAmountText.transform.DOScale(maxSizeDown, extendSizeDuration);
            }
        }
        else
        {
            Debug.LogWarning("Not ScoreAmountText founded !");
        }
    }

    #endregion
}
