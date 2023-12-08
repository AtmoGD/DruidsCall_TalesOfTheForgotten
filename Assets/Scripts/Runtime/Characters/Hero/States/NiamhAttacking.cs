using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhAttacking : NiamhState
{
    public NiamhAttacking(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        niamh.CurrentInput.Attack = false;

        niamh.Finn.Attack();

        niamh.Animator.Play("Attack_Niamh", 0);

        niamh.AttackFeedbacks.PlayFeedbacks();

        return;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        niamh.Rigidbody.velocity = Vector2.Lerp(niamh.Rigidbody.velocity, Vector2.zero, Time.deltaTime * niamh.AttackStopLerpSpeed);

        if (timeInState > niamh.AttackTime / 2)
            Attack();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (timeInState > niamh.AttackTime)
            niamh.ChangeState(niamh.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        niamh.CooldownComponent.AddCooldown(new Cooldown("Attack", niamh.AttackCooldown));
    }

    private void Attack()
    {
        Vector2 dir = niamh.AttackEndPoint.position - niamh.AttackStartPoint.position;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(niamh.AttackStartPoint.position, niamh.AttackRadius, dir, dir.magnitude);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent(out IAttackable enemy))
                enemy.TakeDamage(new Damage(niamh.gameObject, niamh.AttackDamage));
        }
    }
}
