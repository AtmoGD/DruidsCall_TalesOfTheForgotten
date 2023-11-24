using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoving : HeroState
{
    protected bool accelerating = false;
    protected bool updateAccelerationTime = false;
    protected float alreadyAccelerated = 0f;
    protected int lastDir = 1;
    protected bool directionChanged = false;

    protected bool CanMoveHorizontal { get; set; } = true;

    public HeroMoving(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        accelerating = true;

        if (Mathf.Abs(hero.CurrentInput.Move.x) < 0.1f && Mathf.Abs(hero.Rigidbody.velocity.x) < 0.1f)
            alreadyAccelerated = hero.DeccelerationCurve.keys[^1].time;

        lastDir = hero.CurrentInput.LastMoveDirection;
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

        if (!hero.Grounded())
        {
            hero.ChangeState(hero.Falling);
            return;
        }

        if (hero.CurrentInput.Jump && hero.CanJump)
        {
            hero.ChangeState(hero.Jumping);
            return;
        }

        if (Mathf.Abs(hero.Rigidbody.velocity.x) < 0.1f && !accelerating)
        {
            hero.ChangeState(hero.Idle);
            return;
        }

        if (hero.CurrentInput.Attack && hero.CanAttack)
        {
            hero.ChangeState(hero.Attacking);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void CheckDirection()
    {
        if (!hero.ResetAccelerationOnDirectionChange) return;

        if (lastDir != hero.CurrentInput.LastMoveDirection)
        {
            lastDir = hero.CurrentInput.LastMoveDirection;
            directionChanged = true;
        }
    }

    protected virtual void MoveHorizontal()
    {
        float acceleration;

        if (accelerating)
            acceleration = hero.AccelerationCurve.Evaluate(alreadyAccelerated) * Mathf.Abs(hero.CurrentInput.Move.x);
        else
            acceleration = hero.DeccelerationCurve.Evaluate(alreadyAccelerated);

        float speed = hero.CurrentInput.LastMoveDirection * hero.MaxSpeed * acceleration;

        hero.Rigidbody.velocity = new Vector2(speed, hero.Rigidbody.velocity.y);
    }


    protected void UpdateIsAccelerating()
    {
        bool newValue = Mathf.Abs(hero.CurrentInput.Move.x) > 0.1f;
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
        float findValue = Mathf.Abs(hero.Rigidbody.velocity.x) / hero.MaxSpeed;

        if (accelerating)
            alreadyAccelerated = Utils.Remap(findValue, 0, 1, 0, hero.AccelerationCurve.keys[^1].time);
        else
            alreadyAccelerated = Utils.Remap(1 - findValue, 0, 1, 0, hero.DeccelerationCurve.keys[^1].time);

        if (directionChanged)
        {
            directionChanged = false;
            alreadyAccelerated = 0f;
        }

        updateAccelerationTime = false;
    }
}
