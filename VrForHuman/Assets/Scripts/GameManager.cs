using UnityEngine;

[AddComponentMenu("Managers/Game Manager")]
public class GameManager : Etienne.Singleton<GameManager> {
    public bool IsArcadeMode { get; set; }

}
