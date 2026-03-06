using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public float healthMultiplier = 1f;
    public float destroyDelay = 3f;
    
    private bool health_increased;
    private bool dead;
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
    }

    health -= dmg;

    if (health <= 0)
        Die();
}

    void Die()
    {
        dead = true;

        float newAnimosityValue = AnimosityBar.Instance.value + 0.1f;
        
        AnimosityBar.Instance.SetAnimosity(newAnimosityValue);

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