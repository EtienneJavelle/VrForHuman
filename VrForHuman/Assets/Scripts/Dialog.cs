using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialog {
    public string[] Texts => texts;

    [SerializeField] private string[] texts;

    public Etienne.Sound[] DialogSounds => dialogSounds;

    [SerializeField] private Etienne.Sound[] dialogSounds;


    public UnityEvent OnCompleted = new UnityEvent();

    public bool dialogCompleted { get; set; }
}
