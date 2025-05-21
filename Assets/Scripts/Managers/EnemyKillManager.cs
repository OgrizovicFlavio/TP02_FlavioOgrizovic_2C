using UnityEngine;
using TMPro;

public class EnemyKillManager : MonoBehaviourSingleton<EnemyKillManager>
{
    [SerializeField] private TextMeshProUGUI killCounterText;

    private int kills = 0;
    private int targetKills;

    private void Start()
    {
        targetKills = LevelManager.Instance.Current.enemiesToDestroy;
        UpdateUI();
    }

    public void RegisterKill(int amount)
    {
        kills += amount;
        kills = Mathf.Clamp(kills, 0, targetKills);

        UpdateUI();

        if (kills >= targetKills)
        {
            PauseManager.Instance?.WinGame();
        }
    }

    public void ResetKills()
    {
        kills = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (killCounterText != null)
        {
            killCounterText.text = $"KILLS: {kills}/{targetKills}";
        }
    }
}
