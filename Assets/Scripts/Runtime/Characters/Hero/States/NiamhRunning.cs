using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class NiamhRunning : NiamhMoving
{
    public NiamhRunning(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        niamh.Animator.Play("Running_Niamh", 0);
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
