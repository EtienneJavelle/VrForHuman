using UnityEngine;
using UnityEngine.UI;

public class LoadingCircle : MonoBehaviour {
    #region Fields

    private Image image;

    #endregion

    #region Properties

    public float loadingSpeed { get; set; }

    public bool loadingComplete { get; protected set; }

    #endregion

    private void Awake() {
        image = GetComponent<Image>();
        image.fillAmount = 0;
    }

    // Update is called once per frame
    private void Update() {
        if(image.fillAmount < 1) {
            image.fillAmount += Time.deltaTime * loadingSpeed;
        }

        if(image.fillAmount >= 1 && loadingComplete == false) {
            loadingComplete = true;
        }
    }
}
