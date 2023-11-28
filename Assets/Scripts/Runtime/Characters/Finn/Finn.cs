using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Finn : StateMachine
{
    [Header("References")]
    public Hero Hero;
    public Animator Animator;
    public Transform SkinHolder;
    public Rigidbody2D Rb;

    [Header("States")]
    public FinnState Idle;
    public FinnState Following;
    public FinnState Attacking;

    [Header("Settings")]
    public float IdleTime = 5f;
    public float AttackTime = 0.7f;
    public float AttackSpeed = 1f;

    [Header("Feedbacks")]
    public MMF_Player AttackFeedbacks;


    [Header("Movement")]
    public float Speed = 5f;

    private void Awake()
    {
        Idle = new FinnIdle(this);
        Following = new FinnFollowing(this);
        Attacking = new FinnAttacking(this);
    }

    private void Start()
    {
        ChangeState(Following);
    }

    public void Attack()
    {
        ChangeState(Attacking);
    }
}
