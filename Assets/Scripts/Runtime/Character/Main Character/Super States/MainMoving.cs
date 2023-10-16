using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMoving : MainState
{
    private int dir = 1;
    private bool accelerating = false;
    private bool updateAccelerationTime = false;
    private float alreadyAccelerated = 0f;


    public MainMoving(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        base.Enter();

        updateAccelerationTime = true;

        Debug.Log("Entering Moving State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!character.IsGrounded)
            character.ChangeState(character.Falling);

        if (Mathf.Abs(character.Rigidbody.velocity.x) < 0.1f)
            character.ChangeState(character.Idle);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        UpdateIsAccelerating();

        UpdateDirection();

        if (updateAccelerationTime)
            UpdateAccelerationTime();

        MoveHorizontal();
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Moving State");
    }

    private void MoveHorizontal()
    {
        float acceleration = 0f;
        if (accelerating)
        {
            acceleration = character.AccelerationCurve.Evaluate(timeInState + alreadyAccelerated);
        }
        else
        {
            acceleration = character.DeccelerationCurve.Evaluate(timeInState + alreadyAccelerated);
        }
        float speed = dir * character.MaxSpeed * acceleration;
        character.Rigidbody.velocity = new Vector2(speed, character.Rigidbody.velocity.y);
    }

    protected void UpdateDirection()
    {
        if (accelerating)
        {
            dir = character.CurrentInput.Move.x > 0f ? 1 : -1;
        }
        else
        {
            dir = character.Rigidbody.velocity.x > 0f ? 1 : -1;
        }
    }

    protected void UpdateIsAccelerating()
    {
        if (accelerating)
        {
            if (Mathf.Abs(character.CurrentInput.Move.x) < 0.1f)
            {
                if (accelerating)
                    updateAccelerationTime = true;

                accelerating = false;
            }
        }
        else
        {
            if (Mathf.Abs(character.CurrentInput.Move.x) > 0.1f)
            {
                if (!accelerating)
                    updateAccelerationTime = true;
                accelerating = true;
            }
        }
    }

    private void UpdateAccelerationTime()
    {
        float newAccelerationTime = 0f;
        float findValue = 0f;
        if (accelerating)
        {
            findValue = character.AccelerationCurve.keys[^1].time - (character.MaxSpeed / Mathf.Abs(character.Rigidbody.velocity.x));
            newAccelerationTime = Utils.FindTimeInCurve(character.AccelerationCurve, findValue);
        }
        else
        {
            findValue = character.DeccelerationCurve.keys[^1].time - (character.MaxSpeed / Mathf.Abs(character.Rigidbody.velocity.x));
            newAccelerationTime = Utils.FindTimeInCurve(character.DeccelerationCurve, findValue);
        }
        alreadyAccelerated = newAccelerationTime;
    }
}
