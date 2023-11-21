using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttacking : WolfState
{
    private float waitTime = 2f;

    public WolfAttacking(Wolf _wolf, string _animationName = "Attack") : base(_wolf, _animationName) { }

    public override void Enter()
    {
        base.Enter();

        wolf.CurrentInput.Attack = false;

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Entering Attacking State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        wolf.Rigidbody.velocity = Vector2.zero;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (timeInState > waitTime)
        {
            wolf.ChangeState(wolf.Idle);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Exiting Attacking State");
    }
}
