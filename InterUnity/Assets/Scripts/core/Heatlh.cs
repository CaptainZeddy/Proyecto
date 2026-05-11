using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
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
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("death");
        if (destroyOnDeath)
            Destroy(gameObject, 2f); // espera 2 segundos para que se vea la animación
    }
}