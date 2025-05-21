using UnityEngine;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    [Header("Configuration")]
    [SerializeField] private LevelStats currentLevelStats;

    public LevelStats Current => currentLevelStats;

    public void SetLevel(LevelStats newLevelStats)
    {
        currentLevelStats = newLevelStats;
    }

    protected override void OnAwaken()
    {

    }
}
