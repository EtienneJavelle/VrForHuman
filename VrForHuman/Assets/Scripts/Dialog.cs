using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialog {
    public string[] Texts => texts;

    [SerializeField] private string[] texts;

    public UnityEvent dialogCompleted = new UnityEvent();
}
