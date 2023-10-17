using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfIdle : WolfState
{
    public WolfIdle(Wolf _wolf) : base(_wolf) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Idle State");
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

        if (!wolf.Grounded())
            wolf.ChangeState(wolf.Falling);

        if (wolf.CurrentInput.Jump && wolf.CanJump)
            wolf.ChangeState(wolf.Jumping);

        if (Mathf.Abs(wolf.CurrentInput.Move.x) > 0.1f && wolf.MoveActive)
            wolf.ChangeState(wolf.Running);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Idle State");
    }
}
