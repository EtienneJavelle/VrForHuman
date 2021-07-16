using TMPro;
using UnityEngine;

namespace CardiacMassage {
    [System.Serializable]
    public class DepthRank {
        public string Text => text;
        public float Value => value;
        public VertexGradient Colors => colors;

        [SerializeField] private string text;
        [SerializeField] private float value;
        [SerializeField] private VertexGradient colors;
    }
}
