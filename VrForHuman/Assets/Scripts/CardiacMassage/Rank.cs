using UnityEngine;
namespace CardiacMassage {
    [System.Serializable]
    public struct Rank {
        public string DisplayName => displayName;
        public float Points => points;
        public float Offset => offset;

        [SerializeField] private string displayName;
        [SerializeField] private float points;
        [SerializeField] private float offset;

        public Rank(string displayName, float points, float offset) {
            this.displayName = displayName;
            this.points = points;
            this.offset = offset;
        }
    }
}