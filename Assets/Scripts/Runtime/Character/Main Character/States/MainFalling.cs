using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFalling : MainState
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

        if (character.IsGrounded)
            character.ChangeState(character.Landing);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        character.Rigidbody.gravityScale = -character.FallCurve.Evaluate(timeInState);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Falling State");
    }
}
