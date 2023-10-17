using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainJumping : MainMoving
{
    public MainJumping(MainCharacter character) : base(character) { }

    public override void Enter()
    {
        base.Enter();

        character.JumpsLeft--;

        character.Rigidbody.gravityScale = 0f;

        Debug.Log("Entering Jumping State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        MoveUp();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        // base.DoStateChecks();

        if (!character.CurrentInput.Jump && timeInState > character.MinJumpTime)
            character.ChangeState(character.Falling);

        // if (character.IsGrounded() && timeInState > character.MinJumpTime)
        //     character.ChangeState(character.Idle);

        if (timeInState > character.JumpCurve.keys[^1].time)
        {
            Debug.Log("Jumping time exceeded");
            character.ChangeState(character.Falling);
        }
    }

    public override void Exit()
    {
        base.Exit();

        character.CurrentInput.Jump = false;

        // character.Rigidbody.velocity = new Vector2(character.Rigidbody.velocity.x, 0f);

        Debug.Log("Exiting Jumping State");
    }

    private void MoveUp()
    {
        float jumpVelocity = character.JumpCurve.Evaluate(timeInState) * character.JumpHeight;
        character.Rigidbody.velocity = new Vector2(character.Rigidbody.velocity.x, jumpVelocity);
    }
}
