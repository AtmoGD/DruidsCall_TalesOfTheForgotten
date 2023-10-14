using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainJumping : MainState
{
    public MainJumping(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        Debug.Log("Entering Jumping State");
    }

    public override void FrameUpdate()
    {
        Debug.Log("Updating Jumping State");
    }

    public override void PhysicsUpdate()
    {
        Debug.Log("Updating Jumping Physics");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Jumping State");
    }
}
