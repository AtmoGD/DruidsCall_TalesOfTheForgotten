using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdle : HeroState
{
    public HeroIdle(Hero _character, string _animationName = "Idle") : base(_character, _animationName) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hero.Rigidbody.velocity = new Vector2(0, hero.IdleGravity);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (!hero.Grounded())
        {
            hero.ChangeState(hero.Falling);
            return;
        }

        if (hero.CurrentInput.Move.y < hero.FallThroughPlatformThreshold && hero.CanFallThroughPlatform && hero.OnPlatform())
        {
            Debug.Log("<color=pink>Fall Through Platform</color>");
            hero.ChangeState(hero.Falling);
            return;
        }

        if (hero.CurrentInput.Jump && hero.CanJump)
        {
            hero.ChangeState(hero.Jumping);
            return;
        }

        if (Mathf.Abs(hero.CurrentInput.Move.x) > 0.1f && hero.MoveActive)
        {
            hero.ChangeState(hero.Running);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
