using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhIdle : NiamhState
{
    public NiamhIdle(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        niamh.Animator.Play("Idle_Niamh", 0);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        niamh.Rigidbody.velocity = new Vector2(0, niamh.IdleGravity);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (!niamh.Grounded())
        {
            niamh.ChangeState(niamh.Falling);
            return;
        }

        if (niamh.CurrentInput.Move.y < niamh.FallThroughPlatformThreshold && niamh.CanFallThroughPlatform && niamh.OnPlatform())
        {
            niamh.ChangeState(niamh.Falling);
            return;
        }

        if (niamh.CurrentInput.Jump && niamh.CanJump)
        {
            niamh.ChangeState(niamh.Jumping);
            return;
        }

        if (Mathf.Abs(niamh.CurrentInput.Move.x) > 0.1f && niamh.MoveActive)
        {
            niamh.ChangeState(niamh.Running);
            return;
        }

        if (niamh.CurrentInput.Attack && niamh.CanAttack)
        {
            niamh.ChangeState(niamh.Attacking);
            return;
        }

        if (niamh.CurrentInput.Interact)
        {
            niamh.InteractionComponent.Interact();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
