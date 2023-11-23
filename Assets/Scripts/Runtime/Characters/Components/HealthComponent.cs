using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IAttackable
{
    public int MaxHealth = 100;
    public int CurrentHealth;
    public bool CanAttackSelf = false;

    public bool IsDead { get; private set; }

    // public event System.Action OnDeath;
    public UnityEngine.Events.UnityEvent OnDeath;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(Damage damage)
    {
        if (IsDead || (!CanAttackSelf && damage.Attacker == gameObject))
            return;

        CurrentHealth -= damage.DamageAmount;

        if (CurrentHealth <= 0)
        {
            IsDead = true;
            OnDeath?.Invoke();
        }
    }
}
