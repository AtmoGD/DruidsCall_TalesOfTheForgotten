using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdle : HeroState
{
    public HeroIdle(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        hero.Animator.Play("Base Layer.Idle_Hero");
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

        if (hero.CurrentInput.Attack && hero.CanAttack)
        {
            hero.ChangeState(hero.Attacking);
            return;
        }

        if (hero.CurrentInput.Interact)
        {
            hero.InteractionComponent.Interact();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
