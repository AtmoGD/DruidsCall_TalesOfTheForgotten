using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemyGetHit : EnemyGetHit
{
    public PatrolingEnemyGetHit(PatrolingEnemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void FrameUpdate()
    {
        Debug.Log("PatrolingEnemyGetHit");

        if (timeInState >= Enemy.GetHitTime)
            Enemy.ChangeState(((PatrolingEnemy)Enemy).PatrolingState);
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