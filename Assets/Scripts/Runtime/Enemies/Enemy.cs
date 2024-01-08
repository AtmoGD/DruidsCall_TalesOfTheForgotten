using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthComponent))]
public class Enemy : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; } = null;
    public Rigidbody2D Rigidbody2D { get; private set; } = null;
    public HealthComponent HealthComponent { get; private set; } = null;

    [field: SerializeField] public float GetHitTime { get; protected set; } = 0.5f;

    public EnemyState IdleState { get; private set; } = null;
    public EnemyGetHit GetHitState { get; private set; } = null;
    public EnemyState DieState { get; private set; } = null;

    public virtual void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        HealthComponent = GetComponent<HealthComponent>();

        IdleState = new EnemyIdle(this);
        GetHitState = new EnemyGetHit(this);
        DieState = new EnemyDie(this);
    }

    public virtual void Start()
    {
        ChangeState(IdleState);
    }

    public virtual void OnEnable()
    {
        HealthComponent.OnTakeDamage.AddListener(OnGetHit);
        HealthComponent.OnDeath.AddListener(Die);
    }

    public virtual void OnDisable()
    {
        HealthComponent.OnTakeDamage.RemoveListener(OnGetHit);
        HealthComponent.OnDeath.RemoveListener(Die);
    }

    public virtual void OnGetHit(Damage damage)
    {
        Debug.Log("Enemy OnGetHit");

        GetHitState.Damage = damage;
        ChangeState(GetHitState);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
