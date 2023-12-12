using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class NiamhGliding : NiamhMoving
{
    private bool fallThroughPlatform;
    private LayerMask enterLayerMask;
    public NiamhGliding(Niamh _niamh) : base(_niamh) { }

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
        // base.DoStateChecks();

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

        if (!niamh.CurrentInput.Glide)
            niamh.ChangeState(niamh.Falling);

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

        niamh.CooldownComponent.AddCooldown(new Cooldown(niamh.GlideName, niamh.GlideCooldown));
    }

    private void MoveDown()
    {
        float glideSpeed = niamh.FallCurve.Evaluate(timeInState);
        glideSpeed = Mathf.Lerp(niamh.Rigidbody.velocity.y, niamh.GlideSpeed, niamh.FallLerpSpeed * Time.deltaTime);
        niamh.Rigidbody.velocity = new Vector2(niamh.Rigidbody.velocity.x, glideSpeed);
    }
}
