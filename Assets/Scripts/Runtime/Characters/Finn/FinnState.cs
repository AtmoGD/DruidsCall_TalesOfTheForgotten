using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinnState : State
{
    protected Finn finn;

    public FinnState(Finn _finn)
    {
        finn = _finn;
    }

    public override void Enter()
    {
        base.Enter();
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
    }

    public override void Exit()
    {
        base.Exit();
    }
}
