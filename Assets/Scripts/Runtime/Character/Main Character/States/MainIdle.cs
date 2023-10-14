using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIdle : MainState
{
    public MainIdle(MainCharacter character) : base(character)
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
            character.ChangeState(new MainFalling(character));



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
