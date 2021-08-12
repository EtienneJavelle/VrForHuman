using Etienne;
using UnityEngine;

[Requirement(typeof(GameManager))]
public class RunnerManager : MonoBehaviourWithRequirement {
    #region UnityInspector

    [SerializeField] private Runner runner;

    [SerializeField] private GameObject testBasicDummy, cardiacMassageButton, defibrilator;

    public bool isVictim;

    #endregion

    private void Awake() {
        if(isVictim) {
            defibrilator ??= GameObject.FindObjectOfType<Defibrilator>().gameObject;
        }
    }

    private void Start() {
        if(isVictim) {
            ActiveCardiacMassage(false);
        }
    }

    public void ActiveCardiacMassage(bool _value) {
        testBasicDummy.SetActive(_value);
        cardiacMassageButton.SetActive(_value);
        defibrilator.SetActive(_value);

        GameManager.Instance.arrestCardiacStarted = _value;

        runner.gameObject.SetActive(!_value);
    }
}
