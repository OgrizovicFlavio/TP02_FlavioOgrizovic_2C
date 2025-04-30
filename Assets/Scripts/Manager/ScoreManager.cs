using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int scoreToWin = 100;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore = 0;
    private PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

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
            pauseManager.Win();
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
