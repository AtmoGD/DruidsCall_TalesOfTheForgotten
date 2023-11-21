using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfRunning : WolfMoving
{
    public WolfRunning(Wolf _wolf) : base(_wolf) { }

    public override void Enter()
    {
        base.Enter();

        wolf.Animator.Play("Base Layer.Running_Wolf");

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Entering Running State");
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

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Exiting Running State");
    }
}
