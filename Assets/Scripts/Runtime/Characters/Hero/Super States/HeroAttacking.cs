using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttacking : HeroState
{
    public HeroAttacking(Hero _character, string _animationName = "Attacking") : base(_character, _animationName) { }

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
