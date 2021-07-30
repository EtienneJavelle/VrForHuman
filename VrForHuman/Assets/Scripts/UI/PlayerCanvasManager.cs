using UnityEngine;

public class PlayerCanvasManager : MonoBehaviour {
    #region UnityInspector

    [SerializeField] private TextFadingDisplay cityDisplay;
    [SerializeField] private GameObject endSimulationDisplay;

    #endregion

    private void Awake() {
        GameManager.Instance.SetPlayerCanvasManager(this);

        /*if(GameManager.Instance.CardiacMassage == null) {
            ActiveCityDisplay(false);
        } else {
            ActiveCityDisplay(true);
        }*/
    }

    public void ActiveEndSimlulationDisplay(bool _value) {
        endSimulationDisplay.SetActive(_value);
    }

    public void ActiveCityDisplay(bool _value) {
        cityDisplay.gameObject.SetActive(_value);
    }
}
