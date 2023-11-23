using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finn : StateMachine
{
    [Header("References")]
    public Hero Hero;
    public Animator Animator;
    public Transform SkinHolder;
    public Rigidbody2D Rb;

    [Header("States")]
    public FinnState Following;
    public FinnState Idle;

    [Header("Settings")]
    public float IdleTime = 5f;


    [Header("Movement")]
    public float Speed = 5f;

    private void Awake()
    {
        Idle = new FinnIdle(this);
        Following = new FinnFollowing(this);
    }

    private void Start()
    {
        ChangeState(Following);
    }
}
