using TMPro;
using UnityEngine;

public class CityDisplay : MonoBehaviour {
    #region Fields

    private PlayerCanvasManager playerCanvasManager;

    private TextMeshProUGUI cityDisplayText;

    private bool fadeComplete;

    #endregion

    #region UnityInspector

    [SerializeField] private float speedFade = 0.05f;

    #endregion

    private void Awake() {
        playerCanvasManager = GetComponentInParent<PlayerCanvasManager>();

        cityDisplayText = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    private void Start() {
        cityDisplayText.color = new Color(cityDisplayText.color.r, cityDisplayText.color.g, cityDisplayText.color.b, 0);
    }

    private void Update() {
        if(cityDisplayText.color.a < 1) {
            if(fadeComplete == false) {
                cityDisplayText.color = new Color(cityDisplayText.color.r, cityDisplayText.color.g,
                cityDisplayText.color.b, cityDisplayText.color.a + speedFade * Time.deltaTime);
            }
        } else {
            if(fadeComplete == false) {
                fadeComplete = true;
            }
        }

        if(fadeComplete) {
            cityDisplayText.color = new Color(cityDisplayText.color.r, cityDisplayText.color.g,
            cityDisplayText.color.b, cityDisplayText.color.a - speedFade * Time.deltaTime);

            if(cityDisplayText.color.a <= 0) {
                playerCanvasManager.ActiveCityDisplay(false);
            }
        }
    }
}
