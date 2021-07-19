using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public static SceneLoader instance;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(System.Enum _enum) {
        SceneManager.LoadScene((int)(object)_enum);
    }

}
