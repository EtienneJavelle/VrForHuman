using UnityEngine;

public abstract class SpawnJitter : MonoBehaviour {
    public float DiaplayRange => range * transform.lossyScale.y;
    public float Range => range;
    [SerializeField] private float range;
}