using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfJumping : WolfMoving
{
    private LayerMask enterLayerMask;
    public WolfJumping(Wolf _wolf) : base(_wolf) { }

    public override void Enter()
    {
        base.Enter();

        wolf.JumpsLeft--;

        wolf.Rigidbody.gravityScale = 0f;

        if (wolf.CanPhaseThroughPlatforms)
        {
            enterLayerMask = wolf.Rigidbody.excludeLayers;
            wolf.Rigidbody.excludeLayers = wolf.PhaseThroughLayer;
        }

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Entering Jumping State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        MoveUp();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        // base.DoStateChecks(); <------- Nicht machen! Gibt bugs!

        if (!wolf.CurrentInput.Jump && timeInState > wolf.MinJumpTime)
        {
            wolf.ChangeState(wolf.Falling);
            return;
        }

        if (timeInState > wolf.JumpCurve.keys[^1].time)
        {
            wolf.ChangeState(wolf.Falling);
            return;
        }

        if (wolf.HitsTop())
        {
            wolf.ChangeState(wolf.Falling);
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
    }

    public override void Exit()
    {
        base.Exit();

        wolf.CurrentInput.Jump = false;

        if (wolf.CanPhaseThroughPlatforms)
            wolf.Rigidbody.excludeLayers = enterLayerMask;

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Exiting Jumping State");
    }

    private void MoveUp()
    {
        float jumpVelocity = wolf.JumpCurve.Evaluate(timeInState) * wolf.JumpHeight;
        wolf.Rigidbody.velocity = new Vector2(wolf.Rigidbody.velocity.x, jumpVelocity);
    }
}
