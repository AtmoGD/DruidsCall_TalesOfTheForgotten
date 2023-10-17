using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIdle : MainState
{
    public MainIdle(MainCharacter _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Idle State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        character.Rigidbody.velocity = Vector2.zero;
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

        if (Mathf.Abs(character.CurrentInput.Move.x) > 0.1f && character.MoveActive)
            character.ChangeState(character.Running);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Idle State");
    }
}
