using UnityEngine;

public class EndSimulationDisplay : TextFadingDisplay {
    // Update is called once per frame
    public override void Update() {
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
                GameManager.Instance.ScoreScreen();
                playerCanvasManager.ActiveEndSimlulationDisplay(false);
            }
        }
    }
}
