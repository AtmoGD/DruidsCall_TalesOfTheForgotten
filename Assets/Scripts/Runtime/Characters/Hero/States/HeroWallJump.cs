using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWallJump : HeroJumping
{
    private Vector2 wallJumpDirection;

    public HeroWallJump(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        if (!hero.WallJumpConsumesJump)
            hero.JumpsLeft++;

        wallJumpDirection = hero.HitsWallLeft() ? Vector2.right : Vector2.left;
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
        float xVelocity = (hero.WallJumpCurve.Evaluate(timeInState) * wallJumpDirection.x) + hero.Rigidbody.velocity.x;
        hero.Rigidbody.velocity = new Vector2(xVelocity, hero.Rigidbody.velocity.y);
    }
}
