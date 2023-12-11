using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhChargedAttacking : NiamhState
{
    private bool attacked = false;
    public NiamhChargedAttacking(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        niamh.CurrentInput.Attack = false;

        niamh.Finn.Attack();

        niamh.Animator.Play("Attack_Niamh", 0);

        niamh.ChargedAttackFeedbacks?.PlayFeedbacks();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        niamh.Rigidbody.velocity = Vector2.Lerp(niamh.Rigidbody.velocity, Vector2.zero, Time.deltaTime * niamh.AttackStopLerpSpeed);

        if (timeInState > niamh.AttackDoDamageTime && !attacked)
            Attack();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (timeInState > niamh.AttackEndTime)
            niamh.ChangeState(niamh.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        niamh.CooldownComponent.AddCooldown(new Cooldown(niamh.ChargedAttackName, niamh.AttackCooldown));
    }

    private void Attack()
    {
        Vector2 dir = niamh.AttackEndPoint.position - niamh.AttackStartPoint.position;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(niamh.AttackStartPoint.position, niamh.AttackRadius, dir, dir.magnitude);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent(out IAttackable enemy))
                enemy.TakeDamage(new Damage(niamh.gameObject, (int)(niamh.AttackDamage * niamh.ChargedAttackDamageMultiplier), Vector2.zero));
        }

        attacked = true;
    }
}
