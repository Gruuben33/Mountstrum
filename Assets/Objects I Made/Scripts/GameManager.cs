using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public void LoadSceneFromString(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneFromIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}