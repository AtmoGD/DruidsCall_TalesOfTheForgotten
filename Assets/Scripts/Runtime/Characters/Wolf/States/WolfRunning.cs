using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfRunning : WolfMoving
{
    public WolfRunning(Wolf _wolf) : base(_wolf) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Running State");
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

        Debug.Log("Exiting Running State");
    }
}
