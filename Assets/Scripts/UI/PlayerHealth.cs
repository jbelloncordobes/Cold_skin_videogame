using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    public float CurrentHealth { get; private set; }

    public static event Action OnPlayerDied;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        OnPlayerDied?.Invoke();
    }

    public float Normalized => CurrentHealth / maxHealth;
}