using UnityEngine.EventSystems;

namespace UnityEngine.UI {
    [AddComponentMenu(menuName: "UI/Button Audio")]
    [RequireComponent(typeof(Button))]
    [Requirement(typeof(UIAudioManager))]
    public class ButtonAudio : MonoBehaviourWithRequirement, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler {
        [SerializeField] private Etienne.Sound higlightedSound, pressedSound, releasedSound;

        public void OnPointerEnter(PointerEventData eventData) {
            UIAudioManager.Play(higlightedSound);
        }

        public void OnPointerDown(PointerEventData eventData) {
            UIAudioManager.Play(pressedSound);
        }

        public void OnPointerUp(PointerEventData eventData) {
            UIAudioManager.Play(releasedSound);
        }
    }
}