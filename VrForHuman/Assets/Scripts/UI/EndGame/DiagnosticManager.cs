using CardiacMassage;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiagnosticManager : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI totalScoreAmount, maxTimeReachedAmount;
    [SerializeField] private TextMeshProUGUI[] massageRythmResults;
    [SerializeField] private TextMeshProUGUI[] massageDepthResults;

    [Space]

    [SerializeField] private TextMeshProUGUI massageRythmCommentResult, massageDepthCommentResult;

    [Space]

    [SerializeField] private string rythmPerfectComment, rythmSuccessComment, rythmToImproveComment, rythmFailureComment;

    [Space]

    [SerializeField] private string depthPerfectComment, depthSuccessComment, depthNotDeepComment, depthTooDeepComment;

    [Space]

    [SerializeField] private WindowGraph windowGraph;

    private void Start() {

        SetCardiacMassageSavingData();

        UpdateDiagnosticResume();
    }

    private void SetCardiacMassageSavingData() {
        cardiacMassageSaving = GameManager.Instance.CardiacMassageSaving;
    }


    private void SetRythmComment() {
        List<Rank> _listTimingRanks = SetListRanks(cardiacMassageSaving.timingRanks);

        if(_listTimingRanks[0].Text == cardiacMassageSaving.timingRanks[0].Text) {
            massageRythmCommentResult.text = rythmPerfectComment;
        } else if(_listTimingRanks[0].Text == cardiacMassageSaving.timingRanks[1].Text) {
            massageRythmCommentResult.text = rythmSuccessComment;
        } else if(_listTimingRanks[0].Text == cardiacMassageSaving.timingRanks[2].Text ||
              _listTimingRanks[0].Text == cardiacMassageSaving.timingRanks[3].Text) {
            massageRythmCommentResult.text = rythmToImproveComment;
        } else if(_listTimingRanks[0].Text == cardiacMassageSaving.timingRanks[4].Text) {
            massageRythmCommentResult.text = rythmFailureComment;
        }
    }

    private void SetDepthComment() {
        List<Rank> _listDepthRanks = SetListRanks(cardiacMassageSaving.depthRanks);
        if(_listDepthRanks[0].Text == cardiacMassageSaving.depthRanks[0].Text) {
            massageDepthCommentResult.text = depthTooDeepComment;
        } else if(_listDepthRanks[0].Text == cardiacMassageSaving.depthRanks[1].Text) {

            if(cardiacMassageSaving.depthRanks[1].Iterations >= cardiacMassageSaving.depthRanks[0].Iterations * 2 &&
                cardiacMassageSaving.depthRanks[1].Iterations >= cardiacMassageSaving.depthRanks[2].Iterations * 2) {
                massageDepthCommentResult.text = depthPerfectComment;
            } else {
                massageDepthCommentResult.text = depthSuccessComment;
            }

        } else if(_listDepthRanks[0].Text == cardiacMassageSaving.depthRanks[2].Text) {
            massageDepthCommentResult.text = depthNotDeepComment;
        }
    }

    private void SetWindowGraphValues() {
        for(int i = 1; i < cardiacMassageSaving.pushDatas.Count; i++) {
            windowGraph.SetValuesList(cardiacMassageSaving.pushDatas[i]);
        }

        windowGraph.ShowGraph();
    }

    private void UpdateDiagnosticResume() {
        totalScoreAmount.text = cardiacMassageSaving.totalScore.ToString();

        maxTimeReachedAmount.text = (int)(cardiacMassageSaving.maximumTimeReached / 60) + ":" + (int)cardiacMassageSaving.maximumTimeReached;

        for(int i = 0; i < massageRythmResults.Length; i++) {
            massageRythmResults[i].text = cardiacMassageSaving.timingRanks[i].Iterations.ToString();
        }

        for(int i = 0; i < massageDepthResults.Length; i++) {
            massageDepthResults[i].text = cardiacMassageSaving.depthRanks[i].Iterations.ToString();
        }

        SetRythmComment();

        SetDepthComment();

        SetWindowGraphValues();
    }

    private int SortRanksByIterations(Rank _a, Rank _b) {
        int _iterationRankA = _a.Iterations;
        int _iterationRankB = _b.Iterations;
        return _iterationRankA.CompareTo(_iterationRankB);
    }

    private List<Rank> SetListRanks(Rank[] typeRanks) {
        List<Rank> _listRanks = new List<Rank>();
        for(int i = 0; i < typeRanks.Length; i++) {
            _listRanks.Add(typeRanks[i]);
        }

        _listRanks.Sort(SortRanksByIterations);
        _listRanks.Reverse();
        return _listRanks;
    }

    private CardiacMassageSaving cardiacMassageSaving;
}
