using System.Collections.Generic;
using CardiacMassage;
using TMPro;
using UnityEngine;

public class DiagnosticManager : MonoBehaviour {

    //todo: private ou getter
    public TextMeshProUGUI totalScoreAmount, maxTimeReachedAmount;
    public TextMeshProUGUI[] massageRythmResults;
    public TextMeshProUGUI[] massageDepthResults;

    [Space]

    public TextMeshProUGUI massageRythmCommentResult, massageDepthCommentResult;

    [Space]

    public string rythmPerfectComment, rythmSuccessComment, rythmToImproveComment, rythmFailureComment;

    [Space]

    public string depthPerfectComment, depthSuccessComment, depthNotDeepComment, depthTooDeepComment;

    private void Start() {

        SetCardiacMassageSavingData();

        UpdateDiagnosticResume();

        SetRythmComment();
        SetDepthComment();
    }

    private int SortRanksByIterations(Rank _a, Rank _b) {
        int _iterationRankA = _a.Iterations;
        int _iterationRankB = _b.Iterations;
        return _iterationRankA.CompareTo(_iterationRankB);
    }

    //todo Extract methode avec les trucs cummuns aux deux 
    private void SetRythmComment() {
        List<Rank> _listTimingRanks = new List<Rank>();
        for(int i = 0; i < cardiacMassageSavingData.timingRanks.Length; i++) {
            _listTimingRanks.Add(cardiacMassageSavingData.timingRanks[i]);
        }

        _listTimingRanks.Sort(SortRanksByIterations);
        _listTimingRanks.Reverse();

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

    private void SetDepthComment() {
        List<Rank> _listDepthRanks = new List<Rank>();
        for(int i = 0; i < cardiacMassageSavingData.depthRanks.Length; i++) {
            _listDepthRanks.Add(cardiacMassageSavingData.depthRanks[i]);
        }

        _listDepthRanks.Sort(SortRanksByIterations);
        _listDepthRanks.Reverse();

        if(_listDepthRanks[0].Text == cardiacMassageSavingData.depthRanks[0].Text) {
            massageDepthCommentResult.text = depthTooDeepComment;
        } else if(_listDepthRanks[0].Text == cardiacMassageSavingData.depthRanks[1].Text) {

            if(cardiacMassageSavingData.depthRanks[1].Iterations >= cardiacMassageSavingData.depthRanks[0].Iterations * 2 &&
                cardiacMassageSavingData.depthRanks[1].Iterations >= cardiacMassageSavingData.depthRanks[2].Iterations * 2) {
                massageDepthCommentResult.tag = depthPerfectComment;
            } else {
                massageDepthCommentResult.text = depthSuccessComment;
            }

        } else if(_listDepthRanks[0].Text == cardiacMassageSavingData.depthRanks[2].Text) {
            massageDepthCommentResult.text = depthNotDeepComment;
        }
    }

    private void SetCardiacMassageSavingData() {
        cardiacMassageSavingData = GameManager.Instance.CardiacMassageSavingData;
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

    private CardiacMassageSavingData cardiacMassageSavingData;
}
