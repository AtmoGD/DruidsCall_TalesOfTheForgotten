using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhGetHit : NiamhState
{
    public Damage Damage { get; set; } = new Damage(null, 0, Vector2.zero);
    public NiamhGetHit(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        niamh.Rigidbody.gravityScale = niamh.GetHitGravity;

        niamh.CooldownComponent.AddCooldown(new Cooldown(niamh.GetHitName, niamh.GetHitCooldown));

        Knockback();

        niamh.GetHitFeedbacks?.PlayFeedbacks();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (timeInState > niamh.GetHitTime)
            niamh.ChangeState(niamh.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        niamh.Rigidbody.velocity = Vector2.zero;

        niamh.Rigidbody.gravityScale = niamh.IdleGravity;
    }

    public virtual void Knockback()
    {
        niamh.Rigidbody.AddForce(Damage.KnockbackForce, ForceMode2D.Impulse);
    }
}
