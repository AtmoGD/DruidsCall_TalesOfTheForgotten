using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class NiamhJumping : NiamhMoving
{
    protected LayerMask enterLayerMask;
    protected bool consumeJump = true;
    public NiamhJumping(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        niamh.OnJump?.Invoke();

        if (consumeJump)
            niamh.JumpsLeft--;

        niamh.Rigidbody.gravityScale = 0f;

        if (niamh.CanPhaseThroughPlatforms)
        {
            enterLayerMask = niamh.Rigidbody.excludeLayers;
            niamh.Rigidbody.excludeLayers = niamh.PhaseThroughLayer;
        }

        niamh.Animator.Play("Jumping_Niamh", 0);

        niamh.JumpFeedbacks?.PlayFeedbacks();
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

        if (!niamh.CurrentInput.Jump && timeInState > niamh.MinJumpTime)
        {
            niamh.ChangeState(niamh.Falling);
            return;
        }

        if (timeInState > niamh.JumpCurve.keys[^1].time)
        {
            niamh.ChangeState(niamh.Falling);
            return;
        }

        if (niamh.HitsTop())
        {
            niamh.ChangeState(niamh.Falling);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        niamh.CurrentInput.Jump = false;

        if (niamh.CanPhaseThroughPlatforms)
            niamh.Rigidbody.excludeLayers = enterLayerMask;
    }

    // Note: Multiply it with deltaTime?
    protected virtual void MoveUp()
    {
        float jumpVelocity = niamh.JumpCurve.Evaluate(timeInState) * niamh.JumpHeight;
        niamh.Rigidbody.velocity = new Vector2(niamh.Rigidbody.velocity.x, jumpVelocity);
    }
}
