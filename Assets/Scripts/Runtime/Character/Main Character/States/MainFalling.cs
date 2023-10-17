using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFalling : MainMoving
{
    public MainFalling(MainCharacter _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Falling State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        MoveDown();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        // base.DoStateChecks(); <---- This is commented out because we don't want to run the base class's DoStateChecks() method

        if (character.Grounded())
        {
            if (Mathf.Abs(character.Rigidbody.velocity.x) > 0.1f)
                character.ChangeState(character.Running);
            else
                character.ChangeState(character.Idle);
        }

        if (character.CurrentInput.Jump && character.CanJump)
            character.ChangeState(character.Jumping);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Falling State");
    }

    private void MoveDown()
    {
        float fallSpeed = character.FallCurve.Evaluate(timeInState);
        fallSpeed = Mathf.Lerp(character.Rigidbody.velocity.y, fallSpeed, character.FallLerpSpeed * Time.deltaTime);
        character.Rigidbody.velocity = new Vector2(character.Rigidbody.velocity.x, fallSpeed);
    }
}
