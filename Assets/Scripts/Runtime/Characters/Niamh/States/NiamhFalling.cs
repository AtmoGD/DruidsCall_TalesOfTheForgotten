using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhFalling : NiamhMoving
{
    private bool fallThroughPlatform;
    private LayerMask enterLayerMask;
    public NiamhFalling(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        if (niamh.CurrentInput.Move.y < -0.1f && niamh.CanPhaseThroughPlatforms && niamh.OnPlatform())
        {
            fallThroughPlatform = true;
            enterLayerMask = niamh.Rigidbody.excludeLayers;
            niamh.Rigidbody.excludeLayers = niamh.PhaseThroughLayer;
        }

        niamh.Animator.Play("Falling_Niamh", 0);
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
            if (timeInState > niamh.FallThroughPlatformTime)
            {
                niamh.Rigidbody.excludeLayers = enterLayerMask;
                fallThroughPlatform = false;
            }
        }
        else if (niamh.Grounded())
        {
            if (Mathf.Abs(niamh.Rigidbody.velocity.x) > 0.1f)
                niamh.ChangeState(niamh.Running);
            else
                niamh.ChangeState(niamh.Idle);
        }

        if (niamh.CurrentInput.Jump)
        {
            if ((niamh.HitsWallLeft() || niamh.HitsWallRight()) && niamh.CanWallJump)
                niamh.ChangeState(niamh.WallJump);
            else if (niamh.CanJump)
                niamh.ChangeState(niamh.Jumping);
        }

        if (niamh.CurrentInput.Attack && niamh.CanAttack)
        {
            niamh.ChangeState(niamh.ChargingAttack);
            return;
        }

        if (niamh.CurrentInput.Glide && niamh.CanGlide)
        {
            niamh.ChangeState(niamh.Gliding);
            return;
        }

        if (niamh.CurrentInput.Dash && niamh.CanDash)
        {
            niamh.ChangeState(niamh.Dashing);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (fallThroughPlatform)
        {
            niamh.Rigidbody.excludeLayers = enterLayerMask;
            fallThroughPlatform = false;
        }
    }

    private void MoveDown()
    {
        float fallSpeed = niamh.FallCurve.Evaluate(timeInState);
        fallSpeed = Mathf.Lerp(niamh.Rigidbody.velocity.y, fallSpeed, niamh.FallLerpSpeed * Time.deltaTime);
        niamh.Rigidbody.velocity = new Vector2(niamh.Rigidbody.velocity.x, fallSpeed);
    }
}
