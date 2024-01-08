using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : EnemyState
{
    public Damage Damage { get; set; } = null;
    public EnemyGetHit(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.Animator.SetTrigger("GetHit");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Debug.Log("EnemyGetHit");

        if (timeInState >= Enemy.GetHitTime)
            Enemy.ChangeState(Enemy.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public override void Exit()
    {
        base.Exit();

    }
}
