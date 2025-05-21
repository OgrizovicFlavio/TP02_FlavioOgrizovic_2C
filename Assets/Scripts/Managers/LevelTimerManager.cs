using UnityEngine;
using TMPro;
using System.Collections;

public class LevelTimerManager : MonoBehaviourSingleton<LevelTimerManager>
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeRemaining;
    private Coroutine blinkCoroutine;
    private Color normalColor = Color.white;
    private Color warningColor = Color.red;
    private bool timerRunning = false;
    private bool isBlinking = false;

    void Update()
    {
        if (!timerRunning)
            return;

        timeRemaining -= Time.deltaTime;

        if (!isBlinking && timeRemaining <= 10f)
        {
            isBlinking = true;
            blinkCoroutine = StartCoroutine(BlinkTimerText());
        }

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;

            if (blinkCoroutine != null)
                StopCoroutine(blinkCoroutine);

            PauseManager.Instance?.GameOver();
        }

        UpdateUI();
    }

    public void StartTimer()
    {
        timeRemaining = LevelManager.Instance.Current.timeLimit;
        timerRunning = true;
        isBlinking = false;

        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);

        UpdateUI();
    }

    private IEnumerator BlinkTimerText()
    {
        while (true)
        {
            timerText.color = warningColor;
            yield return new WaitForSecondsRealtime(0.2f);

            timerText.color = normalColor;
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

    public void ResetTimer()
    {
        timeRemaining = LevelManager.Instance.Current.timeLimit;
        timerRunning = true;
        isBlinking = false;

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";

            if (!isBlinking)
                timerText.color = normalColor;
        }
    }
}
