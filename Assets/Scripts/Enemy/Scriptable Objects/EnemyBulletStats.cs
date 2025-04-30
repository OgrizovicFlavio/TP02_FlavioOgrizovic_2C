using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBulletStats", menuName = "Data/EnemyBulletStats")]

public class EnemyBulletStats : ScriptableObject
{
    [Header("Stats")]
    public float speed = 10f;
    public float damage = 1f;
    public float lifeTime = 3f;
    public float collisionDelay = 0.05f;
}
