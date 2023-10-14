using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIdle : MainState
{
    public MainIdle(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void FrameUpdate()
    {
        if (!character.IsGrounded)
            character.ChangeState(character.Falling);

        if (character.CurrentInput.Jump)
            character.ChangeState(character.Jumping);

        if (Mathf.Abs(character.CurrentInput.Move.x) > 0.1f)
            character.ChangeState(character.Running);
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
