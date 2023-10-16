using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRunning : MainMoving
{
    public MainRunning(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Running State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // if (!character.IsGrounded)
        //     character.ChangeState(character.Falling);

        // if (Mathf.Abs(character.CurrentInput.Move.x) < 0.1f)
        //     character.ChangeState(character.Deccelerating);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // float speed = character.CurrentInput.Move.x * character.MaxSpeed;
        // character.Rigidbody.velocity = new Vector2(speed, character.Rigidbody.velocity.y);
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        // if (!character.IsGrounded)
        //     character.ChangeState(character.Falling);

        // if (Mathf.Abs(character.Rigidbody.velocity.x) < 0.1f)
        //     character.ChangeState(character.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Running State");
    }
}
