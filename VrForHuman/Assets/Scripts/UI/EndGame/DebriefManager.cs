using CardiacMassage;
using UnityEngine;

public class DebriefManager : MonoBehaviour {

    #region Fields

    private CardiacMassageSaving cardiacMassageSaving;

    #endregion

    #region UnityInspector

    [SerializeField] private WindowGraph windowGraph;

    #endregion

    // Start is called before the first frame update
    private void Start() {
        SetCardiacMassageSavingData();

        SetWindowGraphValues();
    }

    private void SetCardiacMassageSavingData() {
        cardiacMassageSaving = GameManager.Instance.CardiacMassageSaving;
    }

    private void SetWindowGraphValues() {
        for(int i = 1; i < cardiacMassageSaving.pushDatas.Count; i++) {
            windowGraph.SetValuesList(cardiacMassageSaving.pushDatas[i]);
        }

        windowGraph.ShowGraph();
    }
}
