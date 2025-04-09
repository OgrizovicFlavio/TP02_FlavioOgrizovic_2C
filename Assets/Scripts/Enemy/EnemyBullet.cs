using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 4f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private LayerMask playerLayer;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Utilities.CheckLayerInMask(playerLayer, other.gameObject.layer))
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                Vector3 knockbackDir = (other.transform.position - transform.position).normalized; //Calculo la dirección del retroceso (desde la bala hacia el jugador).
                player.TakeDamage((int)damage, knockbackDir);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
