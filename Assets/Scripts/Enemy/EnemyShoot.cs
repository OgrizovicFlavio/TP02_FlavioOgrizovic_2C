using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private float shootInterval = 2.5f;
    [SerializeField] private float shootingRange = 50f;

    private float shootTimer;
    private Transform player;

    private void Update()
    {
        if (player == null)
            return;

        shootTimer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position); //Calculo distancia al jugador.
        if (distance <= shootingRange && shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        Vector3 dir = (player.position - firePoint.position).normalized; //Calculo dirección desde el punto de disparo hacia el jugador.
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(dir)); //Instancio apuntado a esa dirección.

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = dir * shootForce; //Aplico velocidad al rigidbody de la bala en la dirección calculada.
        }
    }

    public void SetTarget(Transform target) //Método para llamar desde otro script para asignarle al jugador como objetivo.
    {
        player = target;
    }
}
