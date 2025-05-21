using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviourSingleton<PauseManager>
{
    public static event Action<bool> OnPause;

    [Header("UI")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private MonoBehaviour[] componentsToDisable;
    [SerializeField] private GameObject crosshair;

    [Header("Level 2")]
    [SerializeField] private LevelStats level2Stats;

    private bool isPaused = false;
    private bool isGameEnded = false;

    protected override void OnAwaken()
    {
        PlayerHealth.OnDie += PlayerHealth_OnDie;
    }

    private void Start()
    {
        Time.timeScale = 1;
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (crosshair != null) crosshair.SetActive(true);
    }

    private void OnDestroy()
    {
        PlayerHealth.OnDie -= PlayerHealth_OnDie;
    }

    private void Update()
    {
        if (isGameEnded) 
            return;

        if (Input.GetKeyDown(KeyCode.P) && !isGameEnded)
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();

            OnPause?.Invoke(isPaused);
        }
    }

    private void PlayerHealth_OnDie()
    {
        GameOver();
    }

    private void EnableComponents()
    {
        foreach (var comp in componentsToDisable)
        {
            if (comp != null)
                comp.enabled = true;
        }

        if (crosshair != null)
            crosshair.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisableComponents()
    {
        foreach (var comp in componentsToDisable)
        {
            if (comp != null)
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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        OnPause?.Invoke(isPaused);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

        EnableComponents();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        OnPause?.Invoke(isPaused);
    }

    public void GoToNextLevel()
    {
        Time.timeScale = 1;

        if (level2Stats != null)
            LevelManager.Instance.SetLevel(level2Stats);

        PoolManager.Instance?.ReturnAllActiveObjects();

        CustomSceneManager.Instance.ChangeSceneTo("Level 2 Scene");
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        isGameEnded = true;
        DisableComponents();
    }

    public void WinGame()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0;
        isGameEnded = true;
        DisableComponents();
    }

    public void RetryGame()
    {
        Time.timeScale = 1;

        if (PoolManager.Instance != null)
            PoolManager.Instance.ReturnAllActiveObjects();

        CoinManager.Instance?.ResetCoins();

        string currentSceneName = SceneManager.GetActiveScene().name;
        if (CustomSceneManager.Instance != null)
        {
            CustomSceneManager.Instance.ReloadSceneImmediately();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
