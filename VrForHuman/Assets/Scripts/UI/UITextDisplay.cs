using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CardiacMassage {
    public class UITextDisplay : MonoBehaviour {

        #region Fields

        private Vector3 minSize;

        #endregion

        #region UnityInspector

        public TextMeshProUGUI uiText;

        [Space]

        public float lifeTime = 1f;
        public float moveSpeed = 1f;

        public float placementJitter = 0.5f;

        [Space]

        [SerializeField] private Vector3 maxSize;
        [SerializeField] private float extendSizeDuration;

        #endregion

        #region Behaviour

        private void Awake()
        {
            minSize = uiText.transform.localScale;
        }

        private void Start()
        {
            uiText.transform.DOScale(maxSize, extendSizeDuration);
        }

        // Update is called once per frame
        private void Update() {
            Destroy(gameObject, lifeTime);
            transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);

            if ((uiText.transform.localScale.x >= maxSize.x && uiText.transform.localScale.y >= maxSize.y
                && uiText.transform.localScale.z >= maxSize.z))
            {
                uiText.transform.DOScale(minSize, extendSizeDuration);
            }
        }

        private void Move() {
            transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0f);
        }

        public void SetPoints(int amount) {
            if(amount >= 0) {
                uiText.text = "+ " + amount.ToString();
            } else {
                uiText.text = amount.ToString();
            }

            Move();
        }

        public void SetText(string _text, VertexGradient _colors) {
            uiText.text = _text;

            uiText.colorGradient = _colors;

            Move();
        }

        #endregion
    }
}