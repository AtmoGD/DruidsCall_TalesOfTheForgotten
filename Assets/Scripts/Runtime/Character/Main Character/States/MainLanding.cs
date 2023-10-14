using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLanding : MainState
{
    public MainLanding(MainCharacter character) : base(character)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Landing State");

        character.ChangeState(new MainIdle(character));
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Debug.Log("Updating Landing State");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Debug.Log("Updating Landing Physics");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Landing State");
    }
}