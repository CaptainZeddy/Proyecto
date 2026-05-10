using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public int damage = 10;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    private NavMeshAgent agent;
    private Health playerHealth;
    private Animator animator;
    private bool playerDetected = false; // ← nuevo

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (!playerDetected) // primera vez que te ve → grita
            {
                playerDetected = true;
                animator.SetTrigger("scream");
            }

            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
        }
        else
        {
            playerDetected = false; // te pierde de vista → resetea sin gritar
            agent.ResetPath();
            animator.SetBool("isWalking", false); // vuelve a idle
        }

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }


    }

    void Attack()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            animator.SetTrigger("attack");
        }
    }
}