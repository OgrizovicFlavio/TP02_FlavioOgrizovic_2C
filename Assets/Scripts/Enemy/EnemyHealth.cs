using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private EnemyStats stats;
    [SerializeField] private EnemyHealthBar enemyHealthBar;
    [SerializeField] private GameObject deathEffect;

    private ScoreManager scoreManager;
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

        if(scoreManager != null)
        {
            scoreManager.AddScore(10);
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
