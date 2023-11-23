using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttacking : HeroState
{
    public HeroAttacking(Hero _character) : base(_character) { }

    public override void Enter()
    {
        base.Enter();

        hero.CurrentInput.Attack = false;

        Attack();
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

        if (timeInState > hero.AttackTime)
            hero.ChangeState(hero.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        hero.CooldownComponent.AddCooldown(new Cooldown("Attack", hero.AttackCooldown));
    }

    private void Attack()
    {
        hero.Animator.Play("Base Layer.Attack_Hero");

        Collider2D[] hits = Physics2D.OverlapCircleAll(hero.AttackPoint.position, hero.AttackRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out IAttackable enemy))
                enemy.TakeDamage(new Damage(hero.gameObject, hero.AttackDamage));
        }

        hero.Finn.Attack();
    }
}
