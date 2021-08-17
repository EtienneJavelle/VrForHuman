using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialog {
    public string[] Texts => texts;

    [SerializeField] private string[] texts;

    public Etienne.Sound DialogSound => dialogSound;

    [SerializeField] private Etienne.Sound dialogSound = new Etienne.Sound(null);


    public UnityEvent OnCompleted = new UnityEvent();

    public bool dialogCompleted { get; set; }
}
