using DG.Tweening;
using Etienne;
using UnityEngine;

[Requirement(typeof(AudioManager))]
public class PlayerCanvasManager : MonoBehaviourWithRequirement {
    #region UnityInspector

    [SerializeField] private TextFadingDisplay cityDisplay;
    [SerializeField] private GameObject endSimulationDisplay;
    [SerializeField] private UnityEngine.UI.Image blood;
    [SerializeField] private Sound hurtSound;

    #endregion

    private bool canBeHurt = true;

    private void Awake() {
        GameManager.Instance.SetPlayerCanvasManager(this);
    }

    public void ActiveEndSimlulationDisplay(bool _value) {
        endSimulationDisplay.SetActive(_value);
    }

    public void ActiveCityDisplay(bool _value) {
        cityDisplay.gameObject.SetActive(_value);
    }

    public void BeHurt() {
        if(!canBeHurt) return;
        canBeHurt = false;
        AudioManager.Play(hurtSound);
        blood.DOComplete();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(blood.DOFade(.8f, .1f).SetEase(Ease.OutElastic));
        sequence.Append(blood.DOFade(.1f, .9f).OnComplete(() => canBeHurt = true));
        sequence.Play();
        GameManager.Instance.ScoreManager.ChangeScore(-1000);
    }
}
