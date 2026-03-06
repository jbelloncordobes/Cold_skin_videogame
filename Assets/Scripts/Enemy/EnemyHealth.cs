using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public float healthMultiplier = 1f;
    public float destroyDelay = 3f;
    [Tooltip("Fraction of max health at which the enemy collapses (plays Die anim but stays alive)")]
    public float collapseThreshold = 0.1f;

    private float maxHealth;
    private bool health_increased;
    private bool dead;
    private bool collapsed;
    private Animator animator;

    void Awake()
    {
        animator =
            GetComponent<Animator>() ??
            GetComponentInChildren<Animator>() ??
            GetComponentInParent<Animator>();
    }

    public void SetMultiplier(float multiplier)
    {
        healthMultiplier = multiplier;
    }

    public void TakeDamage(float dmg)
    {
        if (dead) return;

        if (!health_increased)
        {
            health *= healthMultiplier;
            health_increased = true;
            maxHealth = health;
        }

        health -= dmg;

        // Already collapsed — next hit finishes it off
        if (collapsed)
        {
            if (health <= 0)
                Die();
            return;
        }

        // First time below 10 % → collapse
        if (health <= maxHealth * collapseThreshold)
        {
            Collapse();
            return;
        }

        if (health <= 0)
            Die();
    }

    void Collapse()
    {
        collapsed = true;

        // Play scream animation; speed=0 ensures idle once scream finishes
        animator?.SetTrigger("Scream");
        animator?.SetFloat("speed", 0f);

        var agent = GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.enabled = false;
        }

        foreach (var mb in GetComponents<MonoBehaviour>())
            if (mb != this) mb.enabled = false;

        Debug.Log($"{name}: collapsed (10 % HP threshold reached)");
    }

    void Die()
    {
        dead = true;

        if (AnimosityBar.Instance != null)
        {
            float newAnimosityValue = AnimosityBar.Instance.value + 0.01f;
            AnimosityBar.Instance.SetAnimosity(newAnimosityValue);
        }

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

        Debug.Log($"{name}: die");
    }
}