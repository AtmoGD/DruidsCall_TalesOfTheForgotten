using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinnIdle : FinnState
{
    public FinnIdle(Finn _finn) : base(_finn)
    {
    }

    public override void Enter()
    {
        base.Enter();

        finn.Animator.SetBool("LayingDown", true);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (finn.Hero.Rigidbody.velocity.magnitude > 0.1f)
            finn.ChangeState(finn.Following);
    }

    public override void Exit()
    {
        base.Exit();

        finn.Animator.SetBool("LayingDown", false);
    }
}
