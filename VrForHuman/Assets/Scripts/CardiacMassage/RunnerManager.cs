using Etienne;
using UnityEngine;

[Requirement(typeof(GameManager))]
public class RunnerManager : MonoBehaviourWithRequirement {
    #region UnityInspector

    [SerializeField] private Runner runner;

    [SerializeField] private GameObject testBasicDummy, cardiacMassageButton, defibrilator;

    #endregion

    private void Awake() {
        defibrilator ??= GameObject.FindObjectOfType<Defibrilator>().gameObject;
        ActiveCardiacMassage(false);
    }

    public void ActiveCardiacMassage(bool _value) {
        testBasicDummy.SetActive(_value);
        cardiacMassageButton.SetActive(_value);
        defibrilator.SetActive(_value);

        GameManager.Instance.arrestCardiacStarted = _value;

        runner.gameObject.SetActive(!_value);
    }
}
