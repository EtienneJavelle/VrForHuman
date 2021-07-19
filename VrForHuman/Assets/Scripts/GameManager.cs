using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Properties

    public bool IsArcadeMode { get; set; }

    public static GameManager Instance => instance;
    private static GameManager instance;

    #endregion

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
