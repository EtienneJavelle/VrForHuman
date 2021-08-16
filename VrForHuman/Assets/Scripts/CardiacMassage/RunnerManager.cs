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

    private void Awake() {
        if(isVictim) {
            defibrilator ??= GameObject.FindObjectOfType<CardiacMassage.Defibrilator>().gameObject;
        }
    }

    private void Start() {
        if(isVictim) {
            ActiveCardiacMassage(false);
        }
    }

    public IEnumerator RescueAlertTimer() {
        yield return new WaitForSeconds(15f);

        friendDialogManager.LaunchDialog(1);
    }

    public void ActiveCardiacMassage(bool _value) {
        Debug.Log(gameObject.name + " ActiveCardiacMassage");
        testBasicDummy.SetActive(_value);
        cardiacMassageButton.SetActive(_value);
        defibrilator.SetActive(_value);

        GameManager.Instance.arrestCardiacStarted = _value;

        runner.gameObject.SetActive(!_value);
    }
}
