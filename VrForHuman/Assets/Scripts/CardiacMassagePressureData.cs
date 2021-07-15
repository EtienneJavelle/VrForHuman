using UnityEngine;

[System.Serializable]
public struct CardiacMassagePressureData {
    /// <summary> Time of the pressure since the massage began</summary>
    public float Time => time;
    /// <summary> Depth of the pressure. Ideal pressure depth is 5cm </summary>
    public float Depth => depth;

    [SerializeField] private float depth;
    [SerializeField] private float time;

    public CardiacMassagePressureData(float depth, float time) {
        this.depth = depth;
        this.time = time;
    }
}