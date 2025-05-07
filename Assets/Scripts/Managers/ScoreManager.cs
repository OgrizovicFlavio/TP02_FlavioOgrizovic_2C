using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] private int scoreToWin = 100;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore = 0;
    private PauseManager pauseManager;

    protected override void OnAwaken() { }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int value)
    {
        currentScore += value;
        currentScore = Mathf.Clamp(currentScore, 0, scoreToWin);

        UpdateUI();

        if (currentScore >= scoreToWin)
        {
            PauseManager.Instance?.WinGame();
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {currentScore}/{scoreToWin}";
        }
    }
}
