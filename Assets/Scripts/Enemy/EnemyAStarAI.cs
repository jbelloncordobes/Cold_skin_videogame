using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAStarAI : MonoBehaviour
{
    public float repathInterval = 0.25f;

    [Header("Attack")]
    public float attackRange = 1.6f;
    public float attackCooldown = 1f;
    public float attackDamage = 15f;

    private NavMeshAgent agent;
    private Transform target;
    private PlayerHealth targetHealth;
    private Animator animator;

    private float repathTimer;
    private float attackTimer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        animator =
            GetComponent<Animator>() ??
            GetComponentInChildren<Animator>() ??
            GetComponentInParent<Animator>();

        if (!animator)
            Debug.LogError($"EnemyAStarAI: Animator not found on {name}. Check prefab hierarchy.");
    }

    void Start()
    {
        // Validate NavMeshAgent
        if (!agent.isOnNavMesh)
        {
            Debug.LogError($"EnemyAStarAI: {name} is not on a NavMesh. Make sure the scene has a baked NavMesh and the enemy is placed on it.");
            enabled = false;
            return;
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("EnemyAStarAI: Player not found (tag Player).");
            enabled = false;
            return;
        }

        target = player.transform;
        targetHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (!target || !agent.isOnNavMesh || !agent.enabled) return;

        // Repath
        repathTimer -= Time.deltaTime;
        if (repathTimer <= 0f)
        {
            agent.SetDestination(target.position);
            repathTimer = repathInterval;
        }

        // Attack
        attackTimer -= Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange)
        {
            agent.isStopped = true;

            if (attackTimer <= 0f)
            {
                animator?.SetTrigger("Attack");
                targetHealth?.TakeDamage(attackDamage);
                attackTimer = attackCooldown;
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }
}