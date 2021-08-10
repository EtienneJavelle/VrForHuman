using UnityEngine;
using Valve.VR.InteractionSystem;

public class ButtonUI : UIElement {

    #region Fields

    private LoadingCircle currentLoadingCircle;

    #endregion

    #region UnityInspector

    [SerializeField] private Transform loadingCirclePoint;
    [SerializeField] private GameObject loadingCirclePrefab;
    [SerializeField] private float loadingSpeed = 1f;

    #endregion

    #region Behaviour

    [ContextMenu("Click Button")]
    protected override void OnButtonClick() {
        base.OnButtonClick();
        UnloadButtonUI();
    }

    private void Update() {
        if(currentLoadingCircle != null && currentLoadingCircle.loadingComplete) {
            OnButtonClick();
        }
    }

    protected override void OnHandHoverBegin(Hand hand) {
        base.OnHandHoverBegin(hand);
        LoadButtonUi();
    }

    protected override void OnHandHoverEnd(Hand hand) {
        base.OnHandHoverEnd(hand);
        UnloadButtonUI();
    }

    public void LoadButtonUi() {
        GameObject loadingCircle = Instantiate(loadingCirclePrefab);
        loadingCircle.transform.SetParent(loadingCirclePoint);
        loadingCircle.transform.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        loadingCircle.transform.localPosition = Vector3.zero;
        loadingCircle.transform.localScale = Vector3.one;
        currentLoadingCircle = loadingCircle.GetComponent<LoadingCircle>();
        currentLoadingCircle.loadingSpeed = loadingSpeed;
    }

    public void UnloadButtonUI() {
        if(currentLoadingCircle != null) {
            Destroy(currentLoadingCircle.gameObject);
        }
    }

    #endregion
}
