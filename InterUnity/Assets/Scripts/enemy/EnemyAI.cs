using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public int damage = 10;

    private NavMeshAgent agent;
    private Health playerHealth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<Health>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
        }

        if (distance <= attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        playerHealth.TakeDamage(damage);
    }
}