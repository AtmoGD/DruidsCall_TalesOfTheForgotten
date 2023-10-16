using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLanding : MainState
{
    public MainLanding(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Landing State");

        character.ChangeState(character.Idle);
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
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Landing State");
    }
}
