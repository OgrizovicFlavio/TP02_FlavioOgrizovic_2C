using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        CustomSceneManager.Instance.ChangeSceneTo("Gameplay Scene");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
