using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhDying : NiamhState
{
    private bool hasReset = false;
    public NiamhDying(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        hasReset = false;

        niamh.Animator.Play("Dying_Niamh", 0);

        niamh.DieFeedbacks?.PlayFeedbacks();

        Game.Manager.UIController.PlayDieAnimation(true);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!hasReset && timeInState > niamh.DyingResetTime && Game.Manager.AutoRestartOnDeath)
            ResetNiamh();
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

        Game.Manager.UIController.PlayDieAnimation(false);
    }

    private void ResetNiamh()
    {
        hasReset = true;
        Game.Manager.InitWorld();
    }
}
