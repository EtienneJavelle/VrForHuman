using TMPro;
using UnityEngine;

namespace CardiacMassage {
    [System.Serializable]
    public struct Rank {
        public string Text => text;
        public float Offset => offset;
        public int Points => points;
        public VertexGradient Colors => colors;
        public int Iterations { get; set; }
        public AudioClip Clip => clip;

        [SerializeField] private string text;
        [SerializeField] private float offset;
        [SerializeField] private int points;
        [SerializeField] private VertexGradient colors;
        [SerializeField] private AudioClip clip;
    }
}
