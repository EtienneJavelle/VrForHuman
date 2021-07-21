using Etienne;
using UnityEngine.EventSystems;

namespace UnityEngine.UI {
    [AddComponentMenu(menuName: "UI/Button Audio")]
    [RequireComponent(typeof(Button))]
    [Requirement(typeof(AudioManager2D))]
    public class ButtonAudio : MonoBehaviourWithRequirement, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler {
        [SerializeField]
        private Sound higlightedSound = new Sound(null),
            pressedSound = new Sound(null),
            releasedSound = new Sound(null);

        public void OnPointerEnter(PointerEventData eventData) {
            AudioManager2D.Play(higlightedSound);
        }

        public void OnPointerDown(PointerEventData eventData) {
            AudioManager2D.Play(pressedSound);
        }

        public void OnPointerUp(PointerEventData eventData) {
            AudioManager2D.Play(releasedSound);
        }
    }
}