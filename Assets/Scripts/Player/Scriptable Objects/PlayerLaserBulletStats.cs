using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLaserBulletStats", menuName = "Data/PlayerLaserBulletStats")]

public class PlayerLaserBulletStats : ScriptableObject
{
    [Header("Stats")]
    public float speed = 100f;
    public float damage = 10f;
}
