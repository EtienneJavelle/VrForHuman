using TMPro;
using UnityEngine;

public class TextFadingDisplay : MonoBehaviour {
    #region Fields

    public PlayerCanvasManager playerCanvasManager { get; protected set; }

    public TextMeshProUGUI displayText { get; protected set; }

    public bool fadeComplete { get; protected set; }

    #endregion

    #region Properties

    public bool fading { get; set; }

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
        if(fading) {
            SetDisplayTextColorTransparent();
        } else {
            SetDisplayTextColorVisible();
        }
    }

    public void SetDisplayTextColorTransparent() {
        displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, 0);
    }

    public void SetDisplayTextColorVisible() {
        displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, 1);
    }

    public virtual void Update() {
        if(fading) {
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
                    fading = false;
                    playerCanvasManager.ActiveCityDisplay(false);
                    fadeComplete = false;
                }
            }
        }
    }
}
