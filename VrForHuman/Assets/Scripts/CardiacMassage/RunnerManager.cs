using Etienne;
using System.Collections;
using UnityEngine;

[Requirement(typeof(GameManager))]
public class RunnerManager : MonoBehaviourWithRequirement {
    #region UnityInspector
    [SerializeField] private Runner runner;

    [SerializeField] private GameObject testBasicDummy, cardiacMassageButton, defibrilator;

    public DialogManager DialogManager { get; set; }

    public bool IsVictim;

    #endregion

    public Runner GetRunner() {
        return runner;
    }

    private void Awake() {
        if(IsVictim) {
            defibrilator ??= GameObject.FindObjectOfType<CardiacMassage.Defibrilator>().gameObject;
            DialogManager = GetComponent<DialogManager>();
        }
    }

    private void Start() {
        if(IsVictim) {
            ActiveArrestCardiacSimulation(false);
            ActiveCardiacMassage(false);
            ActiveDefibrilator(false);
        }
    }

    public IEnumerator RescueAlertTimer() {
        Debug.Log("Time before Launch Call Rescue - Coroutine Temp");

        yield return new WaitForSeconds(9f);

        DialogManager.LaunchDialog(1);
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
