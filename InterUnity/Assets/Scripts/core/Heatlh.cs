using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public bool destroyOnDeath = true;
    private PlayerStats stats;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        currentHealth = stats != null ? stats.maxHealth : maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ha muerto");
        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}