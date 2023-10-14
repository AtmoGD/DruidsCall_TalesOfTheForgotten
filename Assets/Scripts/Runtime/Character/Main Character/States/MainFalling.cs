using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFalling : MainState
{
    public MainFalling(MainCharacter character) : base(character)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Falling State");
    }

    public override void FrameUpdate()
    {
        if (character.IsGrounded)
            character.ChangeState(new MainLanding(character));
    }

    public override void PhysicsUpdate()
    {
        character.Rigidbody.gravityScale = -character.FallCurve.Evaluate(timeInState);
    }

    public override void Exit()
    {
        Debug.Log("Exiting Falling State");
    }
}
