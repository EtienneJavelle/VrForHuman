using Etienne;
using System.Collections;
using UnityEngine;

[Requirement(typeof(GameManager))]
public class RunnerManager : MonoBehaviourWithRequirement {
    #region UnityInspector
    [SerializeField] private Runner runner;

    [SerializeField] private GameObject testBasicDummy, cardiacMassageButton, defibrilator;

    public DialogManager friendDialogManager { get; set; }

    public bool isVictim;

    #endregion

    public Runner GetRunner() {
        return runner;
    }

    private void Awake() {
        if(isVictim) {
            defibrilator ??= GameObject.FindObjectOfType<CardiacMassage.Defibrilator>().gameObject;
        }
    }

    private void Start() {
        if(isVictim) {
            ActiveArrestCardiacSimulation(false);
            ActiveCardiacMassage(false);
            ActiveDefibrilator(false);
        }
    }

    public IEnumerator RescueAlertTimer() {
        yield return new WaitForSeconds(7f);

        friendDialogManager.LaunchDialog(1);
    }

    public void ActiveArrestCardiacSimulation(bool _value) {
        Debug.Log(gameObject.name + " ActiveArrestCardiac");
        testBasicDummy.SetActive(_value);
        //cardiacMassageButton.SetActive(_value);
        //defibrilator.SetActive(_value);

        GameManager.Instance.arrestCardiacStarted = _value;

        runner.gameObject.SetActive(!_value);
    }

    public void ActiveCardiacMassage(bool _value) {
        cardiacMassageButton.SetActive(_value);
    }

    public void ActiveDefibrilator(bool _value) {
        defibrilator.SetActive(_value);
    }
}
