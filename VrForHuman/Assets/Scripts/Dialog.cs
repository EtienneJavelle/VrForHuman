using UnityEngine;

[System.Serializable]
public struct Dialog {
    public string[] Texts => texts;

    [SerializeField] private string[] texts;
}
