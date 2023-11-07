using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFalling : HeroMoving
{
    private bool fallThroughPlatform;
    private LayerMask enterLayerMask;
    public HeroFalling(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        if (hero.CurrentInput.Move.y < -0.1f && hero.CanPhaseThroughPlatforms && hero.OnPlatform())
        {
            fallThroughPlatform = true;
            enterLayerMask = hero.Rigidbody.excludeLayers;
            hero.Rigidbody.excludeLayers = hero.PhaseThroughLayer;
        }
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        MoveDown();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        // base.DoStateChecks(); <---- This is commented out because we don't want to run the base class's DoStateChecks() method

        if (fallThroughPlatform)
        {
            if (timeInState > hero.FallThroughPlatformTime)
            {
                hero.Rigidbody.excludeLayers = enterLayerMask;
                fallThroughPlatform = false;
            }
        }
        else if (hero.Grounded())
        {
            if (Mathf.Abs(hero.Rigidbody.velocity.x) > 0.1f)
                hero.ChangeState(hero.Running);
            else
                hero.ChangeState(hero.Idle);
        }

        if (hero.CurrentInput.Jump && hero.CanJump)
        {
            if ((hero.HitsWallLeft() || hero.HitsWallRight()) && hero.CanWallJump)
                hero.ChangeState(hero.WallJump);
            else
                hero.ChangeState(hero.Jumping);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (fallThroughPlatform)
        {
            hero.Rigidbody.excludeLayers = enterLayerMask;
            fallThroughPlatform = false;
        }
    }

    private void MoveDown()
    {
        float fallSpeed = hero.FallCurve.Evaluate(timeInState);
        fallSpeed = Mathf.Lerp(hero.Rigidbody.velocity.y, fallSpeed, hero.FallLerpSpeed * Time.deltaTime);
        hero.Rigidbody.velocity = new Vector2(hero.Rigidbody.velocity.x, fallSpeed);
    }
}
