using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HealthComponent : MonoBehaviour, IAttackable
{
    public UnityEvent<Damage> OnTakeDamage { get; set; } = new UnityEvent<Damage>();
    public UnityEvent OnDeath = new();

    public bool IsImmune = false;

    public int MaxHealth = 100;
    public int CurrentHealth;
    public bool CanAttackSelf = false;
    public bool InvokeTakeDamageOnDeath = true;

    public bool IsDead { get; private set; } = false;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(Damage damage)
    {
        if (IsDead || (!CanAttackSelf && damage.Attacker == gameObject) || IsImmune)
            return;

        CurrentHealth -= damage.DamageAmount;

        if (CurrentHealth <= 0)
        {
            IsDead = true;

            if (InvokeTakeDamageOnDeath)
                OnTakeDamage?.Invoke(damage);

            OnDeath?.Invoke();
        }
        else
            OnTakeDamage?.Invoke(damage);
    }

    public void Reset()
    {
        CurrentHealth = MaxHealth;
        IsDead = false;
    }
}
