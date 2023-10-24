using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdle : HeroState
{
    public HeroIdle(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering Idle State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hero.Rigidbody.velocity = Vector2.zero;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (!hero.Grounded())
            hero.ChangeState(hero.Falling);

        if (hero.CurrentInput.Jump && hero.CanJump)
            hero.ChangeState(hero.Jumping);

        if (Mathf.Abs(hero.CurrentInput.Move.x) > 0.1f && hero.MoveActive)
            hero.ChangeState(hero.Running);
    }

    public override void Exit()
    {
        base.Exit();

        Debug.Log("Exiting Idle State");
    }
}
