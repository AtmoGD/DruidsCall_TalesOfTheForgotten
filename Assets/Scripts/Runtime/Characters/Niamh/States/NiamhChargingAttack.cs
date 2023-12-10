using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhChargingAttack : NiamhState
{
    public NiamhChargingAttack(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        niamh.ChargingAttackFeedbacks?.PlayFeedbacks();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        niamh.Rigidbody.velocity = new Vector2(0f, 0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (niamh.CooldownComponent.HasCooldown(niamh.ChargedAttackName))
        {
            niamh.ChangeState(niamh.Attacking);
            return;
        }

        if (!niamh.CurrentInput.Attack)
        {
            if (timeInState < niamh.ChargedAttackTimeMin)
            {
                niamh.ChangeState(niamh.Attacking);
            }
            else
            {
                niamh.ChangeState(niamh.ChargedAttack);
            }
        }

        if (timeInState > niamh.ChargedAttackTimeMax)
        {
            niamh.ChangeState(niamh.ChargedAttack);
        }
    }

    public override void Exit()
    {
        base.Exit();

        niamh.ChargingAttackFeedbacks?.StopFeedbacks();
    }
}
