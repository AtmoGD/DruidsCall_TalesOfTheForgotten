using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDeccelerating : MainState
{
    int dir = 1;
    float alreadyDeccelerated = 0f;
    public MainDeccelerating(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        base.Enter();

        dir = character.Rigidbody.velocity.x > 0 ? 1 : -1;

        float findValue = character.DeccelerationCurve.keys[^1].time - (character.MaxSpeed / Mathf.Abs(character.Rigidbody.velocity.x));
        alreadyDeccelerated = Utils.FindTimeInCurve(character.DeccelerationCurve, findValue);

        Debug.Log("Entering Deccelerating State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!character.IsGrounded)
            character.ChangeState(character.Falling);

        if (Mathf.Abs(character.CurrentInput.Move.x) > 0.1f)
            character.ChangeState(character.Accelerating);

        if (timeInState + alreadyDeccelerated >= character.DeccelerationCurve.keys[^1].time)
            character.ChangeState(character.Idle);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        float decceleration = character.DeccelerationCurve.Evaluate(timeInState + alreadyDeccelerated);
        float speed = dir * character.MaxSpeed * decceleration;
        character.Rigidbody.velocity = new Vector2(speed, character.Rigidbody.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Deccelerating State");
    }
}
