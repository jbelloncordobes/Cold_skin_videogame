using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public float destroyDelay = 3f;

    private bool dead;
    private Animator animator;

    void Awake()
    {
        animator =
            GetComponent<Animator>() ??
            GetComponentInChildren<Animator>() ??
            GetComponentInParent<Animator>();
    }

    public void TakeDamage(float dmg)
    {
        if (dead) return;

        health -= dmg;
        if (health <= 0) Die();
    }

    void Die()
    {
        dead = true;

        animator?.SetTrigger("Die");

        var agent = GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.enabled = false;
        }

        // Disable AI scripts but keep Animator alive
        foreach (var mb in GetComponents<MonoBehaviour>())
            if (mb != this) mb.enabled = false;

        Destroy(gameObject, destroyDelay);
    }
}