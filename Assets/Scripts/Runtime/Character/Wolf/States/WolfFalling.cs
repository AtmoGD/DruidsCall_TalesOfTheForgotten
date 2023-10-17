using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFalling : WolfMoving
{
    public WolfFalling(Wolf _wolf) : base(_wolf) { }

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

        if (wolf.IsGrounded())
        {
            if (Mathf.Abs(wolf.Rigidbody.velocity.x) > 0.1f)
                wolf.ChangeState(wolf.Running);
            else
                wolf.ChangeState(wolf.Idle);
        }

        if (wolf.CurrentInput.Jump && wolf.CanJump)
            wolf.ChangeState(wolf.Jumping);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Falling State");
    }

    private void MoveDown()
    {
        float fallSpeed = wolf.FallCurve.Evaluate(timeInState);
        fallSpeed = Mathf.Lerp(wolf.Rigidbody.velocity.y, fallSpeed, wolf.FallLerpSpeed * Time.deltaTime);
        wolf.Rigidbody.velocity = new Vector2(wolf.Rigidbody.velocity.x, fallSpeed);
    }
}
