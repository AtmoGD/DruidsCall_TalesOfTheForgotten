using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWallJump : HeroJumping
{
    private Vector2 wallJumpDirection;

    public HeroWallJump(Hero _character, string _animationName = "WallJump") : base(_character, _animationName) { }

    public override void Enter()
    {
        consumeJump = hero.WallJumpConsumesJump;

        base.Enter();

        wallJumpDirection = hero.HitsWallLeft() ? Vector2.right : Vector2.left;

        if (hero.WallJumpResetsJumps) hero.ResetJumpsLeft();
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
        float xVelocity = hero.WallJumpCurve.Evaluate(timeInState) * wallJumpDirection.x;

        float acceleration;

        if (accelerating)
            acceleration = hero.AccelerationCurve.Evaluate(alreadyAccelerated) * Mathf.Abs(hero.CurrentInput.Move.x);
        else
            acceleration = hero.DeccelerationCurve.Evaluate(alreadyAccelerated);

        float speed = hero.CurrentInput.LastMoveDirection * hero.MaxSpeed * acceleration;

        speed = Mathf.Abs(speed) > Mathf.Abs(xVelocity) ? speed : xVelocity;

        // hero.Rigidbody.velocity = new Vector2(xVelocity, hero.Rigidbody.velocity.y);
        hero.Rigidbody.velocity = new Vector2(speed, hero.Rigidbody.velocity.y);
    }
}
