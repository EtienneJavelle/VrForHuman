using TMPro;
using UnityEngine;

public class TextFadingDisplay : MonoBehaviour {
    #region Fields

    public PlayerCanvasManager playerCanvasManager { get; protected set; }

    public TextMeshProUGUI displayText { get; protected set; }

    public bool fadeComplete { get; protected set; }

    #endregion

    #region UnityInspector

    public float speedFade;

    #endregion

    private void Awake() {
        playerCanvasManager = GetComponentInParent<PlayerCanvasManager>();

        displayText = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    private void Start() {
        displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, 0);
    }

    public virtual void Update() {
        if(displayText.color.a < 1) {
            if(fadeComplete == false) {
                displayText.color = new Color(displayText.color.r, displayText.color.g,
                displayText.color.b, displayText.color.a + speedFade * Time.deltaTime);
            }
        } else {
            if(fadeComplete == false) {
                fadeComplete = true;
            }
        }

        if(fadeComplete) {
            displayText.color = new Color(displayText.color.r, displayText.color.g,
            displayText.color.b, displayText.color.a - speedFade * Time.deltaTime);

            if(displayText.color.a <= 0) {
                playerCanvasManager.ActiveCityDisplay(false);
            }
        }
    }
}
