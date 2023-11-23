using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Enemy : StateMachine
{
    [SerializeField] private HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        healthComponent.OnDeath.AddListener(Die);
    }

    private void OnDisable()
    {
        healthComponent.OnDeath.RemoveListener(Die);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
