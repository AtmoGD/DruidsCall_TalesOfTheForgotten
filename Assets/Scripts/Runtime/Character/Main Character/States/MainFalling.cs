using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFalling : MainMoving
{
    public MainFalling(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Falling State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        character.Rigidbody.gravityScale = -character.FallCurve.Evaluate(timeInState);
    }

    public override void DoStateChecks()
    {
        // base.DoStateChecks(); <---- This is commented out because we don't want to run the base class's DoStateChecks() method

        if (character.IsGrounded)
            character.ChangeState(character.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Falling State");
    }
}
