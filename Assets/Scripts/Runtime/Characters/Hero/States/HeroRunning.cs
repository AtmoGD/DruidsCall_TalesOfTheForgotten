using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRunning : HeroMoving
{
    public HeroRunning(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        hero.Animator.SetBool("IsRunning", true);

        if (hero.ShowDebugLogs)
            Debug.Log("Hero: Entering Running State");
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

        hero.Animator.SetBool("IsRunning", false);

        if (hero.ShowDebugLogs)
            Debug.Log("Hero: Exiting Running State");
    }
}
