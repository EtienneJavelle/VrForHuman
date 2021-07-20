using UnityEngine.SceneManagement;

public class SceneLoader : Etienne.Singleton<SceneLoader> {

    public void ChangeScene(System.Enum _enum) {
        SceneManager.LoadScene((int)(object)_enum);
    }

}
