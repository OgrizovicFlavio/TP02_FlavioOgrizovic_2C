using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private int killGoal = 10;
    [SerializeField] private TextMeshProUGUI enemyCounterText;

    private int currentKills = 0;
    private PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    private void Start()
    {
        UpdateUI();
    }

    public void CountKill()
    {
        currentKills++;
        UpdateUI();
        Debug.Log("Enemigos derrotados: " + currentKills);

        if (currentKills >= killGoal)
        {
            pauseManager.Win();
        }
    }

    private void UpdateUI()
    {
        if (enemyCounterText != null)
        {
            enemyCounterText.text = $"ENEMIES: {currentKills}/{killGoal}";
        }
    }
}
