using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private MonoBehaviour[] componentsToDisable;
    [SerializeField] private GameObject crosshair;

    private bool isPaused = false;
    private bool isGameEnded = false;

    private void Start()
    {
        Time.timeScale = 1;
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (crosshair != null) crosshair.SetActive(true);
    }

    private void Update()
    {
        if (isGameEnded) 
            return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    private void DisableComponents()
    {
        foreach (var comp in componentsToDisable)
        {
            comp.enabled = false;
        }

        if (crosshair != null)
            crosshair.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;

        DisableComponents();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

        foreach (var component in componentsToDisable)
        {
            component.enabled = true;
        }

        if (crosshair != null)
            crosshair.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        isGameEnded = true;
        DisableComponents();

    }

    public void Win()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0;
        isGameEnded = true;
        DisableComponents();
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
