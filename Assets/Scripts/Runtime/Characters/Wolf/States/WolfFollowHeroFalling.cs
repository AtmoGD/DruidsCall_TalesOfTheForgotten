using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFollowHeroFalling : WolfFalling
{
    public WolfFollowHeroFalling(Wolf _wolf, string _animationName = "Falling") : base(_wolf, _animationName) { }

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
        base.DoStateChecks();
    }

    public override void Exit()
    {
        base.Exit();

        wolf.CurrentInput.OverrideMove = false;
        wolf.CurrentInput.OverrideLastMoveDirection = false;
        wolf.CurrentInput.OverrideJump = false;
    }
}
