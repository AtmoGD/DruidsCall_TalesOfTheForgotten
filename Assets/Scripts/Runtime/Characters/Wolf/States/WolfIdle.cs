using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfIdle : WolfState
{
    public WolfIdle(Wolf _wolf) : base(_wolf) { }

    public override void Enter()
    {
        base.Enter();

        wolf.Animator.Play("Base Layer.Idle_Wolf");

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Entering Idle State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        wolf.Rigidbody.velocity = new Vector2(0, wolf.IdleGravity);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (!wolf.Grounded())
        {
            wolf.ChangeState(wolf.Falling);
            return;
        }

        if (wolf.CurrentInput.Jump && wolf.CanJump)
        {
            wolf.ChangeState(wolf.Jumping);
            return;
        }

        if (Mathf.Abs(wolf.CurrentInput.Move.x) > 0.1f && wolf.MoveActive)
        {
            wolf.ChangeState(wolf.Running);
            return;
        }

        if (wolf.CurrentInput.TeleportToHero)
        {
            wolf.ChangeState(wolf.TeleportToHero);
            return;
        }

        if (wolf.CurrentInput.Attack)
        {
            wolf.ChangeState(wolf.Attacking);
            return;
        }

        return;
    }

    public override void Exit()
    {
        base.Exit();

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Exiting Idle State");
    }
}
