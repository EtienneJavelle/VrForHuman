using CardiacMassage;
using TMPro;
using UnityEngine;

public class DiagnosticManager : MonoBehaviour {

    private CardiacMassageSavingData cardiacMassageSavingData;

    public TextMeshProUGUI totalScoreAmount, maxTimeReachedAmount;
    public TextMeshProUGUI[] massageRythmResults;
    public TextMeshProUGUI[] massageDepthResults;

    // Start is called before the first frame update
    private void Start() {

        SetCardiacMassageSavingData();
    }

    private void SetCardiacMassageSavingData() {
        cardiacMassageSavingData = GameManager.Instance.GetCardiacMassageSavingData();
    }


}
