using UnityEngine;

public class EnemyDroneHealth : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private EnemyDroneStats stats;
    [SerializeField] private EnemyHealthBar enemyHealthBar;
    [SerializeField] private GameObject deathEffect;

    private float currentHealth;

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        enemyHealthBar.UpdateHealthBar(stats.maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = stats.maxHealth;
        enemyHealthBar.SetHealthBar(stats.maxHealth, currentHealth);
    }

    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Entity entity = GetComponent<Entity>();
        if (entity != null)
        {
            entity.Die(); //Función Die() de clase base
        }
    }

    public void ForceDie()
    {
        currentHealth = 0;
        enemyHealthBar.UpdateHealthBar(stats.maxHealth, currentHealth);

        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
}
