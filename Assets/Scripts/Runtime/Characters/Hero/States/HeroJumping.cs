using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class HeroJumping : HeroMoving
{
    protected LayerMask enterLayerMask;
    protected bool consumeJump = true;
    public HeroJumping(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        hero.onJump?.Invoke();

        if (consumeJump)
            hero.JumpsLeft--;

        hero.Rigidbody.gravityScale = 0f;

        if (hero.CanPhaseThroughPlatforms)
        {
            enterLayerMask = hero.Rigidbody.excludeLayers;
            hero.Rigidbody.excludeLayers = hero.PhaseThroughLayer;
        }

        hero.Animator.Play("Base Layer.Jumping_Hero");
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

        if (!hero.CurrentInput.Jump && timeInState > hero.MinJumpTime)
        {
            hero.ChangeState(hero.Falling);
            return;
        }

        if (timeInState > hero.JumpCurve.keys[^1].time)
        {
            hero.ChangeState(hero.Falling);
            return;
        }

        if (hero.HitsTop())
        {
            hero.ChangeState(hero.Falling);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        hero.CurrentInput.Jump = false;

        if (hero.CanPhaseThroughPlatforms)
            hero.Rigidbody.excludeLayers = enterLayerMask;
    }

    // Note: Multiply it with deltaTime?
    protected virtual void MoveUp()
    {
        float jumpVelocity = hero.JumpCurve.Evaluate(timeInState) * hero.JumpHeight;
        hero.Rigidbody.velocity = new Vector2(hero.Rigidbody.velocity.x, jumpVelocity);
    }
}
