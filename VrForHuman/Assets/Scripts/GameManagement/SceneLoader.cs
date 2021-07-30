using UnityEngine.SceneManagement;

public class SceneLoader : Etienne.Singleton<SceneLoader> {

    private void Start() {
        ChangeScene(Scenes.MainMenu);
    }

    public void ChangeScene(System.Enum _enum) {
        SceneManager.LoadScene((int)(object)_enum);
    }

}
