using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;

        if (CustomSceneManager.Instance != null)
        {
            CustomSceneManager.Instance.ChangeSceneTo("Main Menu Scene");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu Scene");
        }
    }
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}


