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

        enterLayerMask = wolf.Rigidbody.excludeLayers;

        wolf.Rigidbody.excludeLayers = wolf.PhaseThroughLayer;

        Debug.Log("Entering Jumping State");
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
            wolf.ChangeState(wolf.Falling);

        if (timeInState > wolf.JumpCurve.keys[^1].time)
            wolf.ChangeState(wolf.Falling);

        if (wolf.HitsTop())
            wolf.ChangeState(wolf.Falling);
    }

    public override void Exit()
    {
        base.Exit();

        wolf.CurrentInput.Jump = false;

        wolf.Rigidbody.excludeLayers = enterLayerMask;

        Debug.Log("Exiting Jumping State");
    }

    private void MoveUp()
    {
        float jumpVelocity = wolf.JumpCurve.Evaluate(timeInState) * wolf.JumpHeight;
        wolf.Rigidbody.velocity = new Vector2(wolf.Rigidbody.velocity.x, jumpVelocity);
    }
}
