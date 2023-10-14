using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFalling : CharacterState
{
    public CharacterFalling(Character character) : base(character)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Falling State");
    }

    public override void FrameUpdate()
    {
        Debug.Log("Updating Falling State");
    }

    public override void PhysicsUpdate()
    {
        Debug.Log("Updating Falling Physics");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Falling State");
    }
}
