using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainJumping : MainMoving
{
    private LayerMask enterLayerMask;
    public MainJumping(MainCharacter _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        character.JumpsLeft--;

        character.Rigidbody.gravityScale = 0f;

        enterLayerMask = character.Rigidbody.excludeLayers;

        character.Rigidbody.excludeLayers = character.PhaseThroughLayer;

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
        // base.DoStateChecks(); <------- Nicht machen! Gibt bugs!

        if (!character.CurrentInput.Jump && timeInState > character.MinJumpTime)
            character.ChangeState(character.Falling);

        if (timeInState > character.JumpCurve.keys[^1].time)
            character.ChangeState(character.Falling);

        if (character.HitsTop())
            character.ChangeState(character.Falling);
    }

    public override void Exit()
    {
        base.Exit();

        character.CurrentInput.Jump = false;

        character.Rigidbody.excludeLayers = enterLayerMask;

        Debug.Log("Exiting Jumping State");
    }

    private void MoveUp()
    {
        float jumpVelocity = character.JumpCurve.Evaluate(timeInState) * character.JumpHeight;
        character.Rigidbody.velocity = new Vector2(character.Rigidbody.velocity.x, jumpVelocity);
    }
}
