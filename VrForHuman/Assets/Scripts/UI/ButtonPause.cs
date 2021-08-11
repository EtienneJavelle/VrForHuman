using UnityEngine;
using UnityEngine.UI;
public class ButtonPause : ButtonUI {

    #region Fields

    private Image image;

    #endregion

    #region UnityInspector

    [SerializeField] private Sprite pauseSprite, replaySprite;

    #endregion

    public override void Awake() {
        base.Awake();
        image = GetComponent<Image>();
    }

    public void SetPauseSprite(bool _value) {
        if(_value == true && image != null) {
            image.sprite = replaySprite;
        } else if(image != null) {
            image.sprite = pauseSprite;
        }
    }
}
