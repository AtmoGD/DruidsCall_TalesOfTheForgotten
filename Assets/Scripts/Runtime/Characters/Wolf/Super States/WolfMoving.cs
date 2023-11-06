using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMoving : WolfState
{
    protected bool accelerating = false;
    protected bool updateAccelerationTime = false;
    protected float alreadyAccelerated = 0f;
    protected int lastDir = 1;
    protected bool directionChanged = false;

    protected bool CanMoveHorizontal { get; set; } = true;

    public WolfMoving(Wolf _wolf) : base(_wolf) { }

    public override void Enter()
    {
        base.Enter();

        if (Mathf.Abs(wolf.CurrentInput.Move.x) < 0.1f && Mathf.Abs(wolf.Rigidbody.velocity.x) < 0.1f)
            alreadyAccelerated = wolf.DeccelerationCurve.keys[^1].time;

        lastDir = wolf.CurrentInput.LastMoveDirection;
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

        MoveHorizontal(wolf.CurrentInput.Move.x);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (!wolf.Grounded())
        {
            wolf.ChangeState(wolf.Falling);
            return;
        }

        if (wolf.CurrentInput.Jump && wolf.CanJump)
        {
            wolf.ChangeState(wolf.Jumping);
            return;
        }

        if (Mathf.Abs(wolf.Rigidbody.velocity.x) < 0.1f && !accelerating)
        {
            wolf.ChangeState(wolf.Idle);
            return;
        }

        if (wolf.CurrentInput.TeleportToHero)
        {
            wolf.ChangeState(wolf.TeleportToHero);
            return;
        }

        if (wolf.CurrentInput.Attack)
        {
            wolf.ChangeState(wolf.Attacking);
            return;
        }

        if (!wolf.IsControlledByPlayer && wolf.CurrentInput.HeroJumped)
        {
            wolf.ChangeState(wolf.FollowHeroJump);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected virtual void CheckDirection()
    {
        if (!wolf.ResetAccelerationOnDirectionChange) return;

        if (lastDir != wolf.CurrentInput.LastMoveDirection)
        {
            lastDir = wolf.CurrentInput.LastMoveDirection;
            directionChanged = true;
        }
    }

    protected void MoveHorizontal(float _dir)
    {
        float acceleration;

        if (accelerating)
            acceleration = wolf.AccelerationCurve.Evaluate(alreadyAccelerated) * Mathf.Abs(_dir);
        else
            acceleration = wolf.DeccelerationCurve.Evaluate(alreadyAccelerated);

        float speed = wolf.CurrentInput.LastMoveDirection * wolf.MaxSpeed * acceleration;

        if (wolf.IncreaseSpeedBasedOnHeroDistance && !wolf.InFollowRadius)
        {
            float heroDistance = Mathf.Abs(wolf.transform.position.x - wolf.FollowTransform.position.x);
            float heroDistanceFactor = Utils.Remap(heroDistance, 0, wolf.TeleportRadius, 1, 2);
            speed *= heroDistanceFactor;
        }

        wolf.Rigidbody.velocity = new Vector2(speed, wolf.Rigidbody.velocity.y);
    }


    protected void UpdateIsAccelerating()
    {
        bool newValue = Mathf.Abs(wolf.CurrentInput.Move.x) > 0.1f;

        if (newValue != accelerating)
        {
            updateAccelerationTime = true;
            accelerating = newValue;
        }
    }

    protected void UpdateAccelerationTime()
    {
        float findValue = Mathf.Abs(wolf.Rigidbody.velocity.x) / wolf.MaxSpeed;

        if (accelerating)
            alreadyAccelerated = Utils.Remap(findValue, 0, 1, 0, wolf.AccelerationCurve.keys[^1].time);
        else
            alreadyAccelerated = Utils.Remap(1 - findValue, 0, 1, 0, wolf.DeccelerationCurve.keys[^1].time);

        if (directionChanged)
        {
            directionChanged = false;
            alreadyAccelerated = 0f;
        }

        updateAccelerationTime = false;
    }
}
