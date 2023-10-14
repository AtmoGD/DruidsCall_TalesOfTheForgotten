using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRunning : MainState
{
    private bool isAccelerating = false;
    private float lastAccelerationChange = 0f;

    public MainRunning(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        lastAccelerationChange = Time.time;
    }

    public override void FrameUpdate()
    {
        if (Mathf.Abs(character.CurrentInput.Move.x) > 0.1f && !isAccelerating)
        {
            isAccelerating = true;
            lastAccelerationChange = Time.time;
        }
        else if (Math.Abs(character.CurrentInput.Move.x) < 0.1f && isAccelerating)
        {
            isAccelerating = false;
            lastAccelerationChange = Time.time;
        }


        if (!character.IsGrounded)
            character.ChangeState(character.Falling);

        if (Mathf.Abs(character.CurrentInput.Move.x) < 0.1f && character.Rigidbody.velocity.magnitude < 0.1f)
        {
            character.ChangeState(character.Idle);
            Debug.Log("move: " + character.CurrentInput.Move.x + " vel: " + character.Rigidbody.velocity.magnitude);
        }
    }

    public override void PhysicsUpdate()
    {
        float speed = character.CurrentInput.Move.x * character.SpeedMultiplier;
        float speedMultiplier = 0f;

        if (isAccelerating)
            speedMultiplier = character.AccelerationCurve.Evaluate(Time.time - lastAccelerationChange);
        else
            speedMultiplier = character.DecelerationCurve.Evaluate(Time.time - lastAccelerationChange);

        character.Rigidbody.velocity = new Vector2(speed * speedMultiplier, character.Rigidbody.velocity.y);
    }

    public override void Exit()
    {
        Debug.Log("Exiting Running State");
    }
}
