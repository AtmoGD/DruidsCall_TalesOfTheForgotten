using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhMoving : NiamhState
{
    protected bool accelerating = false;
    protected bool updateAccelerationTime = false;
    protected float alreadyAccelerated = 0f;
    protected int lastDir = 1;
    protected bool directionChanged = false;

    protected bool CanMoveHorizontal { get; set; } = true;

    public NiamhMoving(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        accelerating = true;

        if (Mathf.Abs(niamh.CurrentInput.Move.x) < 0.1f && Mathf.Abs(niamh.Rigidbody.velocity.x) < 0.1f)
            alreadyAccelerated = niamh.DeccelerationCurve.keys[^1].time;

        lastDir = niamh.CurrentInput.LastMoveDirection;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!CanMoveHorizontal) return;

        alreadyAccelerated += Time.deltaTime;

        CheckDirection();

        UpdateIsAccelerating();

        if (updateAccelerationTime)
            UpdateAccelerationTime();

        MoveHorizontal();
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

        if (niamh.CurrentInput.Jump && niamh.CanJump)
        {
            niamh.ChangeState(niamh.Jumping);
            return;
        }

        if (Mathf.Abs(niamh.Rigidbody.velocity.x) < 0.1f && !accelerating)
        {
            niamh.ChangeState(niamh.Idle);
            return;
        }

        if (niamh.CurrentInput.Attack && niamh.CanAttack)
        {
            niamh.ChangeState(niamh.ChargingAttack);
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
    }

    private void CheckDirection()
    {
        if (!niamh.ResetAccelerationOnDirectionChange) return;

        if (lastDir != niamh.CurrentInput.LastMoveDirection)
        {
            lastDir = niamh.CurrentInput.LastMoveDirection;
            directionChanged = true;
        }
    }

    protected virtual void MoveHorizontal()
    {
        float acceleration;

        if (accelerating)
            acceleration = niamh.AccelerationCurve.Evaluate(alreadyAccelerated) * Mathf.Abs(niamh.CurrentInput.Move.x);
        else
            acceleration = niamh.DeccelerationCurve.Evaluate(alreadyAccelerated);

        float speed = niamh.CurrentInput.LastMoveDirection * niamh.MaxSpeed * acceleration;

        niamh.Rigidbody.velocity = new Vector2(speed, niamh.Rigidbody.velocity.y);

        niamh.Animator.SetFloat("SpeedX", Mathf.Abs(niamh.Rigidbody.velocity.x) / niamh.MaxSpeed);
    }


    protected void UpdateIsAccelerating()
    {
        bool newValue = Mathf.Abs(niamh.CurrentInput.Move.x) > 0.1f;
        if (newValue != accelerating)
        {
            updateAccelerationTime = true;
            accelerating = newValue;
        }
    }

    // NOTE: This is not the true value. With remap we are getting a linear value but its still a good approximation
    // For exact value you can use utils.FindTimeInCurve but it is more expensive and not really tested
    protected void UpdateAccelerationTime()
    {
        float findValue = Mathf.Abs(niamh.Rigidbody.velocity.x) / niamh.MaxSpeed;

        if (accelerating)
            alreadyAccelerated = Utils.Remap(findValue, 0, 1, 0, niamh.AccelerationCurve.keys[^1].time);
        else
            alreadyAccelerated = Utils.Remap(1 - findValue, 0, 1, 0, niamh.DeccelerationCurve.keys[^1].time);

        if (directionChanged)
        {
            directionChanged = false;
            alreadyAccelerated = 0f;
        }

        updateAccelerationTime = false;
    }
}
