using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFollowHeroJump : WolfJumping
{
    public WolfFollowHeroJump(Wolf _wolf) : base(_wolf) { }

    public override void Enter()
    {
        base.Enter();

        wolf.CurrentInput.OverrideMove = true;
        wolf.CurrentInput.OverrideLastMoveDirection = true;
        wolf.CurrentInput.OverrideJump = true;
    }

    public override void FrameUpdate()
    {
        wolf.CurrentInput.OverrideMoveValue = wolf.Hero.CurrentInput.Move;
        wolf.CurrentInput.OverrideLastMoveDirectionValue = wolf.Hero.CurrentInput.LastMoveDirection;
        wolf.CurrentInput.OverrideJumpValue = wolf.Hero.CurrentInput.Jump;

        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        // base.DoStateChecks();

        if (!wolf.CurrentInput.Jump && timeInState > wolf.MinJumpTime)
        {
            wolf.ChangeState(wolf.FollowHeroFalling);
            return;
        }

        if (timeInState > wolf.JumpCurve.keys[^1].time)
        {
            wolf.ChangeState(wolf.FollowHeroFalling);
            return;
        }

        if (wolf.HitsTop())
        {
            wolf.ChangeState(wolf.FollowHeroFalling);
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

        wolf.CurrentInput.HeroJumped = false;

        wolf.CurrentInput.OverrideMove = false;
        wolf.CurrentInput.OverrideLastMoveDirection = false;
        wolf.CurrentInput.OverrideJump = false;
    }
}
