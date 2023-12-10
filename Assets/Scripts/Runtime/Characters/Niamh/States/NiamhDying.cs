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
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!hasReset && timeInState > niamh.DyingResetTime)
        {
            hasReset = true;
            ResetNiamh();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (timeInState > niamh.DyingTime)
            niamh.ChangeState(niamh.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void ResetNiamh()
    {
        Game.Manager.InitWorld();
    }
}
