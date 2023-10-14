using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : CharacterState
{
    public CharacterIdle(Character character) : base(character)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void FrameUpdate()
    {
        Debug.Log("Updating Idle State");

        if (!character.IsGrounded)
            character.ChangeState(new CharacterFalling(character));



    }

    public override void PhysicsUpdate()
    {
        Debug.Log("Updating Idle Physics");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
