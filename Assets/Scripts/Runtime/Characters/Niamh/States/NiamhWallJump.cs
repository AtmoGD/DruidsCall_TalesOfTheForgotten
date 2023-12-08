using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhWallJump : NiamhJumping
{
    public Vector2 wallJumpDirection;

    public NiamhWallJump(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        consumeJump = niamh.WallJumpConsumesJump;

        base.Enter();

        wallJumpDirection = niamh.HitsWallLeft() ? Vector2.right : Vector2.left;

        if (niamh.WallJumpResetsJumps) niamh.ResetJumpsLeft();
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

    protected override void MoveHorizontal()
    {
        float xVelocity = niamh.WallJumpCurve.Evaluate(timeInState) * wallJumpDirection.x;

        float acceleration;
        if (accelerating)
            acceleration = niamh.AccelerationCurve.Evaluate(alreadyAccelerated) * Mathf.Abs(niamh.CurrentInput.Move.x);
        else
            acceleration = niamh.DeccelerationCurve.Evaluate(alreadyAccelerated);

        float speed = niamh.CurrentInput.LastMoveDirection * niamh.MaxSpeed * acceleration;
        speed = Mathf.Abs(speed) > Mathf.Abs(xVelocity) ? speed : xVelocity;

        niamh.Rigidbody.velocity = new Vector2(speed, niamh.Rigidbody.velocity.y);
    }
}
