using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIdle : MainState
{
    public MainIdle(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Idle State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (!character.IsGrounded)
            character.ChangeState(character.Falling);

        if (character.CurrentInput.Jump)
            character.ChangeState(character.Jumping);

        if (Mathf.Abs(character.CurrentInput.Move.x) > 0.1f)
            character.ChangeState(character.Moving);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Idle State");
    }
}
