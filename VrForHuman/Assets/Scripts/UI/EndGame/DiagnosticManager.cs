using CardiacMassage;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiagnosticManager : MonoBehaviour {

    private CardiacMassageSavingData cardiacMassageSavingData;
    public List<Rank> _listTimingRanks = new List<Rank>();

    public TextMeshProUGUI totalScoreAmount, maxTimeReachedAmount;
    public TextMeshProUGUI[] massageRythmResults;
    public TextMeshProUGUI[] massageDepthResults;

    [Space]

    public TextMeshProUGUI massageRythmCommentResult, massageDepthCommentResult;

    [Space]

    public string rythmPerfectComment, rythmSuccessComment, rythmToImproveComment, rythmFailureComment;

    [Space]

    public string depthPerfectComment, depthSuccessComment, depthNotDeepComment, depthTooDeepComment;

    // Start is called before the first frame update
    private void Start() {

        SetCardiacMassageSavingData();

        UpdateDiagnosticResume();


        for(int i = 0; i < cardiacMassageSavingData.timingRanks.Length; i++) {
            _listTimingRanks.Add(cardiacMassageSavingData.timingRanks[i]);
        }

        _listTimingRanks.Sort(SortRanksByIterations);

        if(_listTimingRanks[0].Text == cardiacMassageSavingData.timingRanks[0].Text) {
            massageRythmCommentResult.text = rythmPerfectComment;
        } else if(_listTimingRanks[0].Text == cardiacMassageSavingData.timingRanks[1].Text) {
            massageRythmCommentResult.text = rythmSuccessComment;
        } else if(_listTimingRanks[0].Text == cardiacMassageSavingData.timingRanks[2].Text ||
              _listTimingRanks[0].Text == cardiacMassageSavingData.timingRanks[3].Text) {
            massageRythmCommentResult.text = rythmToImproveComment;
        } else if(_listTimingRanks[0].Text == cardiacMassageSavingData.timingRanks[4].Text) {
            massageRythmCommentResult.text = rythmFailureComment;
        }
    }

    private int SortRanksByIterations(Rank _a, Rank _b) {
        int _iterationRankA = _a.Iterations;
        int _iterationRankB = _b.Iterations;
        return _iterationRankA.CompareTo(_iterationRankB);
    }

    private void SetCardiacMassageSavingData() {
        cardiacMassageSavingData = GameManager.Instance.GetCardiacMassageSavingData();
    }

    private void UpdateDiagnosticResume() {
        totalScoreAmount.text = cardiacMassageSavingData.totalScore.ToString();

        maxTimeReachedAmount.text = (int)(cardiacMassageSavingData.maximumTimeReached / 60) + ":" + (int)cardiacMassageSavingData.maximumTimeReached;

        for(int i = 0; i < massageRythmResults.Length; i++) {
            massageRythmResults[i].text = cardiacMassageSavingData.timingRanks[i].Iterations.ToString();
        }

        for(int i = 0; i < massageDepthResults.Length; i++) {
            massageDepthResults[i].text = cardiacMassageSavingData.depthRanks[i].Iterations.ToString();
        }
    }
}
