using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class HeroRunning : HeroMoving
{
    public HeroRunning(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        hero.Animator.Play("Base Layer.Running_Hero");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // float speedX = Utils.Remap01(Mathf.Abs(hero.Rigidbody.velocity.x), 0, hero.MaxSpeed);

        // hero.Animator.SetFloat("SpeedX", speedX);
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
