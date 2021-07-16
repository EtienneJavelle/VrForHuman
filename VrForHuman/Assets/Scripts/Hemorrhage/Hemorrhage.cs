using UnityEngine;
using Valve.VR.InteractionSystem;

public class Hemorrhage : MonoBehaviour 
{
    #region Properties

    [SerializeField] private Interactable interactable;
    [SerializeField] private GameObject particle;

    private bool isPressed;
    private float pressValue;

    #endregion

    #region UnityInspector

    public float pressValueMax = 100;

    #endregion

    #region Behaviour

    private void Start() 
    {
        //interactable.onAttachedToHand += _ => particle.SetActive(false);
        interactable.onAttachedToHand += _ => isPressed = true;

        interactable.onDetachedFromHand += _ => particle.SetActive(true);
        interactable.onDetachedFromHand += _ => isPressed = false;
    }

    private void Update()
    {
        if(isPressed)
        {
            pressValue += Time.deltaTime;

            if(pressValue >= pressValueMax)
            {
                particle.SetActive(false);
            }
        }
        else
        {
            pressValue = 0; 
        }
    }

    #endregion
}
