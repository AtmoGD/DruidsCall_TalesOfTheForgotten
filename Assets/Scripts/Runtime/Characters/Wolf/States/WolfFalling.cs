using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFalling : WolfMoving
{
    public WolfFalling(Wolf _wolf) : base(_wolf) { }

    public override void Enter()
    {
        base.Enter();

        wolf.Animator.Play("Base Layer.Falling_Wolf");

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Entering Falling State");
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

        if (wolf.Grounded())
        {
            if (Mathf.Abs(wolf.Rigidbody.velocity.x) > 0.1f)
            {
                wolf.ChangeState(wolf.Running);
                return;
            }
            else
            {
                wolf.ChangeState(wolf.Idle);
                return;

            }
        }

        if (wolf.CurrentInput.Jump && wolf.CanJump)
        {
            wolf.ChangeState(wolf.Jumping);
            return;
        }

        if (wolf.CurrentInput.TeleportToHero)
        {
            wolf.ChangeState(wolf.TeleportToHero);
            return;
        }

        if (wolf.CurrentInput.Attack)
        {
            wolf.ChangeState(wolf.Attacking);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Exiting Falling State");
    }

    private void MoveDown()
    {
        float fallSpeed = wolf.FallCurve.Evaluate(timeInState);
        fallSpeed = Mathf.Lerp(wolf.Rigidbody.velocity.y, fallSpeed, wolf.FallLerpSpeed * Time.deltaTime);
        wolf.Rigidbody.velocity = new Vector2(wolf.Rigidbody.velocity.x, fallSpeed);
    }
}
