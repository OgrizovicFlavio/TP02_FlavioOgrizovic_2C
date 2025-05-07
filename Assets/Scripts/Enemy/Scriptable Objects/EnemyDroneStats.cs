using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDroneStats", menuName = "Data/EnemyDroneStats")]
public class EnemyDroneStats : ScriptableObject
{
    [Header("Movement")]
    public float speed = 20f;
    public float force = 2f;

    [Header("Shoot")]
    public float fireRate = 2f;
    public float shootingRange = 100f;

    [Header("Health")]
    public float maxHealth = 100f;

    [Header("Navigation")]
    public float stoppingDistance = 1.5f;
    public float rotationSpeed = 5f;
}
