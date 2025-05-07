using UnityEngine;

[CreateAssetMenu(fileName = "PlayerExplosiveBulletStats", menuName = "Data/PlayerExplosiveBulletStats")]

public class PlayerExplosiveBulletStats : ScriptableObject
{
    [Header("Stats")]
    public float speed = 20f;
    public float damage = 50f;
    public float explosionRadius = 5f;
}
