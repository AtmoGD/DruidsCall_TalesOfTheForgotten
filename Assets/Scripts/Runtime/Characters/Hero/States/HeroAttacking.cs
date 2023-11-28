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

        hero.Animator.Play("Base Layer.Attack_Hero");

        return;
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
        Vector2 dir = hero.AttackStartPoint.position - hero.AttackEndPoint.position;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(hero.AttackStartPoint.position, hero.AttackRadius, dir, dir.magnitude);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent(out IAttackable enemy))
                enemy.TakeDamage(new Damage(hero.gameObject, hero.AttackDamage));
        }
    }
}
