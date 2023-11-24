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

        hero.Finn.Attack();

        // hero.ChangeState(hero.Idle);

        return;

        hero.Animator.Play("Base Layer.Attack_Hero");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        hero.Rigidbody.velocity = Vector2.Lerp(hero.Rigidbody.velocity, Vector2.zero, Time.deltaTime * hero.AttackStopLerpSpeed);

        if (timeInState > hero.AttackTime / 2)
            Attack();
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

        Collider2D[] hits = Physics2D.OverlapCircleAll(hero.AttackPoint.position, hero.AttackRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out IAttackable enemy))
                enemy.TakeDamage(new Damage(hero.gameObject, hero.AttackDamage));
        }

    }
}
