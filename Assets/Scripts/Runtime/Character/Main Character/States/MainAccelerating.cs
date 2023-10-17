using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAccelerating : MainState
{
    float alreadyAccelerated = 0f;
    public MainAccelerating(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        base.Enter();

        float findValue = character.AccelerationCurve.keys[^1].time - (character.MaxSpeed / Mathf.Abs(character.Rigidbody.velocity.x));
        alreadyAccelerated = Utils.FindTimeInCurve(character.AccelerationCurve, findValue);

        Debug.Log("Entering Accelerating State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!character.IsGrounded())
            character.ChangeState(character.Falling);

        if (Mathf.Abs(character.CurrentInput.Move.x) < 0.1f)
            character.ChangeState(character.Deccelerating);

        if (timeInState + alreadyAccelerated >= character.AccelerationCurve.keys[^1].time)
            character.ChangeState(character.Running);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        float acceleration = character.AccelerationCurve.Evaluate(timeInState + alreadyAccelerated);
        float speed = character.CurrentInput.Move.x * character.MaxSpeed * acceleration;
        character.Rigidbody.velocity = new Vector2(speed, character.Rigidbody.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Accelerating State");
    }
}
