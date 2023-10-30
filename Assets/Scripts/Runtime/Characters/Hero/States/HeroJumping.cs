using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJumping : HeroMoving
{
    private LayerMask enterLayerMask;
    public HeroJumping(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        hero.JumpsLeft--;

        hero.Rigidbody.gravityScale = 0f;

        if (hero.CanPhaseThroughPlatforms)
        {
            enterLayerMask = hero.Rigidbody.excludeLayers;
            hero.Rigidbody.excludeLayers = hero.PhaseThroughLayer;
        }

        if (hero.ShowDebugLogs)
            Debug.Log("Hero: Entering Jumping State");
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
            hero.ChangeState(hero.Falling);

        if (timeInState > hero.JumpCurve.keys[^1].time)
            hero.ChangeState(hero.Falling);

        if (hero.HitsTop())
            hero.ChangeState(hero.Falling);
    }

    public override void Exit()
    {
        base.Exit();

        hero.CurrentInput.Jump = false;

        if (hero.CanPhaseThroughPlatforms)
            hero.Rigidbody.excludeLayers = enterLayerMask;

        if (hero.ShowDebugLogs)
            Debug.Log("Hero: Exiting Jumping State");
    }

    private void MoveUp()
    {
        float jumpVelocity = hero.JumpCurve.Evaluate(timeInState) * hero.JumpHeight;
        hero.Rigidbody.velocity = new Vector2(hero.Rigidbody.velocity.x, jumpVelocity);
    }
}
