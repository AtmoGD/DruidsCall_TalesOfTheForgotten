using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFalling : HeroMoving
{
    public HeroFalling(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        if (hero.ShowDebugLogs)
            Debug.Log("Hero: Entering Falling State");
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

        if (hero.Grounded())
        {
            if (Mathf.Abs(hero.Rigidbody.velocity.x) > 0.1f)
                hero.ChangeState(hero.Running);
            else
                hero.ChangeState(hero.Idle);
        }

        if (hero.CurrentInput.Jump && hero.CanJump)
            hero.ChangeState(hero.Jumping);
    }

    public override void Exit()
    {
        base.Exit();

        if (hero.ShowDebugLogs)
            Debug.Log("Hero: Exiting Falling State");
    }

    private void MoveDown()
    {
        float fallSpeed = hero.FallCurve.Evaluate(timeInState);
        fallSpeed = Mathf.Lerp(hero.Rigidbody.velocity.y, fallSpeed, hero.FallLerpSpeed * Time.deltaTime);
        hero.Rigidbody.velocity = new Vector2(hero.Rigidbody.velocity.x, fallSpeed);
    }
}
