using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBulletStats", menuName = "Data/EnemyBulletStats")]

public class EnemyBulletStats : ScriptableObject
{
    [Header("Stats")]
    public float speed = 20f;
    public float damage = 25f;
    public float lifeTime = 3f;
    public float collisionDelay = 0.05f;
}
