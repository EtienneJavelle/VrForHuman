using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CardiacMassage {
    public class UITextDisplay : MonoBehaviour {
        public Canvas Canvas => canvas ??= GetComponentInParent<Canvas>();

        #region Fields
        private Canvas canvas;
        #endregion

        #region UnityInspector
        [SerializeField] private TextMeshProUGUI uiText;

        [Space]

        [SerializeField] private float lifeTime = 1f;
        [SerializeField] private float moveSpeed = 1f;

        [Space]

        [SerializeField] private Vector3 maxSize;
        [SerializeField] private float extendSizeDuration;
        #endregion

        #region Behaviour

        private void Start() {
            uiText.transform.DOPunchScale(maxSize, extendSizeDuration);
        }

        private void Update() {
            Destroy(gameObject, lifeTime);
            transform.position += new Vector3(0f, moveSpeed * Time.deltaTime * Canvas.transform.lossyScale.y, 0f);
        }

        public void SetPoints(int amount) {
            uiText.text = (amount >= 0 ? "+ " : "") + amount.ToString();
        }

        public void SetText(string _text, VertexGradient _colors) {
            uiText.text = _text;
            uiText.colorGradient = _colors;
        }
        #endregion
    }
}