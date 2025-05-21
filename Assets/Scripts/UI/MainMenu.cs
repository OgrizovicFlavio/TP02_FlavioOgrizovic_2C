using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelStats level1Stats;

    public void PlayGame()
    {
        Time.timeScale = 1;

        LevelManager.Instance.SetLevel(level1Stats);

        PoolManager.Instance?.ReturnAllActiveObjects();
        CustomSceneManager.Instance.ChangeSceneTo("Level 1 Scene");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
