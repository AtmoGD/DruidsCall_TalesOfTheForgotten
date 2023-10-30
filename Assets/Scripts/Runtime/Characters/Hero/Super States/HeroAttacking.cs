using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttacking : HeroState
{
    public HeroAttacking(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        if (hero.ShowDebugLogs)
            Debug.Log("Hero: Entering Attacking State");
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

        if (hero.ShowDebugLogs)
            Debug.Log("Hero: Exiting Attacking State");
    }
}
