using UnityEngine;

public class PlayerCanvasManager : MonoBehaviour {
    #region UnityInspector

    [SerializeField] private CityDisplay cityDisplay;

    #endregion

    private void Awake() {
        GameManager.Instance.SetPlayerCanvasManager(this);

        if(GameManager.Instance.CardiacMassage == null) {
            ActiveCityDisplay(false);
        } else {
            ActiveCityDisplay(true);
        }
    }

    public void ActiveCityDisplay(bool _value) {
        cityDisplay.gameObject.SetActive(_value);
    }
}
