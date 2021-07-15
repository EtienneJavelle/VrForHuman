using TMPro;
using UnityEngine;

namespace CardiacMassage {
    public class UITextDisplay : MonoBehaviour {
        public TextMeshProUGUI uiText;

        public float lifeTime = 1f;
        public float moveSpeed = 1f;

        public float placementJitter = 0.5f;

        // Update is called once per frame
        private void Update() {
            Destroy(gameObject, lifeTime);
            transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
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

        public void SetText(string _text) {
            uiText.text = _text;

            Move();
        }
    }
}