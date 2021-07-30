using UnityEngine;

public class RunnerManager : MonoBehaviour {
    #region UnityInspector

    [SerializeField] private Runner runner;

    [SerializeField] private GameObject testBasicDummy;
    [SerializeField] private GameObject cardiacMassageButton;

    #endregion

    private void Start() {
        ActiveCardiacMassage(false);
    }

    public void ActiveCardiacMassage(bool _value) {
        testBasicDummy.SetActive(_value);
        cardiacMassageButton.SetActive(_value);

        GameManager.Instance.arrestCardiacStarted = _value;

        runner.gameObject.SetActive(!_value);
    }
}
