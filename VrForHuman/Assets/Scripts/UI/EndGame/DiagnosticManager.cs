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

    private List<Rank> SetListRanks(Rank[] typeRanks) {
        List<Rank> _listRanks = new List<Rank>();
        for(int i = 0; i < typeRanks.Length; i++) {
            _listRanks.Add(typeRanks[i]);
        }

        _listRanks.Sort(SortRanksByIterations);
        _listRanks.Reverse();
        return _listRanks;
    }

    private void SetRythmComment() {
        List<Rank> _listTimingRanks = SetListRanks(cardiacMassageSavingData.timingRanks);

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
        List<Rank> _listDepthRanks = SetListRanks(cardiacMassageSavingData.timingRanks);

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
