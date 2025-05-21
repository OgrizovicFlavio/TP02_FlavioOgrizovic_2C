using UnityEngine;

[CreateAssetMenu(fileName = "LevelStats", menuName = "Data/LevelStats")]
public class LevelStats : ScriptableObject
{
    [Header("Pool Settings")]
    public int bulletsPullSize = 20;
    public int enemyPoolSize = 20;
    public int npcPoolSize = 20;
    public int impactEffectPoolSize = 20;

    [Header("Limits")]
    public float xMin = -100f;
    public float xMax = 100f;
    public float yMin = 0f;
    public float yMax = 50f;
    public float zMin = -75f;
    public float zMax = 75f;

    [Header("Damage Settings")]
    public float playerCollisionDamage = 5f;

    [Header("Spawner Intervals")]
    public float enemySpawnInterval = 5f;
    public float npcSpawnInterval = 7f;

    [Header("Drone Spawner Settings")]
    public int enemiesToDestroy = 10;
    public int initialEnemySpawnCount = 3;

    [Header("NPC Spawner Settings")]
    public int npcSpawnLimit = 10;
    public int initialNPCSpawnCount = 4;
    public float npcSafeRadius = 8f;

    [Header("Timer Settings")]
    public float timeLimit = 120f;
}
