using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMoving : MainState
{
    private bool accelerating = false;
    private bool updateAccelerationTime = false;
    private float alreadyAccelerated = 0f;
    private int lastDir = 1;
    private bool directionChanged = false;

    protected bool CanMoveHorizontal { get; set; } = true;

    public MainMoving(MainCharacter _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        if (Mathf.Abs(character.CurrentInput.Move.x) < 0.1f && Mathf.Abs(character.Rigidbody.velocity.x) < 0.1f)
            alreadyAccelerated = character.DeccelerationCurve.keys[^1].time;

        lastDir = character.CurrentInput.LastMoveDirection;
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

        if (!character.Grounded())
            character.ChangeState(character.Falling);

        if (character.CurrentInput.Jump && character.CanJump)
            character.ChangeState(character.Jumping);

        if (Mathf.Abs(character.Rigidbody.velocity.x) < 0.1f && !accelerating)
            character.ChangeState(character.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void CheckDirection()
    {
        if (!character.ResetAccelerationOnDirectionChange) return;

        if (lastDir != character.CurrentInput.LastMoveDirection)
        {
            lastDir = character.CurrentInput.LastMoveDirection;
            directionChanged = true;
        }
    }

    private void MoveHorizontal()
    {
        float acceleration;

        if (accelerating)
            acceleration = character.AccelerationCurve.Evaluate(alreadyAccelerated) * Mathf.Abs(character.CurrentInput.Move.x);
        else
            acceleration = character.DeccelerationCurve.Evaluate(alreadyAccelerated);

        float speed = character.CurrentInput.LastMoveDirection * character.MaxSpeed * acceleration;

        character.Rigidbody.velocity = new Vector2(speed, character.Rigidbody.velocity.y);
    }


    protected void UpdateIsAccelerating()
    {
        bool newValue = Mathf.Abs(character.CurrentInput.Move.x) > 0.1f;
        if (newValue != accelerating)
        {
            updateAccelerationTime = true;
            accelerating = newValue;
        }
    }

    private void UpdateAccelerationTime()
    {
        float findValue = Mathf.Abs(character.Rigidbody.velocity.x) / character.MaxSpeed;

        if (accelerating)
            alreadyAccelerated = Utils.Remap(findValue, 0, 1, 0, character.AccelerationCurve.keys[^1].time);
        else
            alreadyAccelerated = Utils.Remap(1 - findValue, 0, 1, 0, character.DeccelerationCurve.keys[^1].time);

        if (directionChanged)
        {
            directionChanged = false;
            alreadyAccelerated = 0f;
        }

        updateAccelerationTime = false;
    }
}
