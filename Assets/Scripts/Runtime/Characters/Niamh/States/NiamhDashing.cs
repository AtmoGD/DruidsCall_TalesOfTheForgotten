using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiamhDashing : NiamhState
{
    private int direction = 0;
    private List<IAttackable> alreadyAttacked = new List<IAttackable>();

    public NiamhDashing(Niamh _niamh) : base(_niamh) { }

    public override void Enter()
    {
        base.Enter();

        direction = niamh.DirectionComponent.Direction.x > 0f ? 1 : -1;

        alreadyAttacked.Clear();

        niamh.HealthComponent.IsImmune = true;

        niamh.Collider.isTrigger = true;

        niamh.Animator.Play("Dashing_Niamh", 0);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Dash();

        Attack();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (timeInState > niamh.DashTime)
            niamh.ChangeState(niamh.Idle);

        if (niamh.HitsWallLeft() || niamh.HitsWallRight())
            niamh.ChangeState(niamh.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        niamh.HealthComponent.IsImmune = false;

        niamh.Collider.isTrigger = false;

        niamh.CooldownComponent.AddCooldown(new Cooldown(niamh.DashName, niamh.DashCooldown));
    }

    private void Dash()
    {
        float speed = niamh.DashCurve.Evaluate(timeInState) * direction * niamh.DashSpeed;

        niamh.Rigidbody.velocity = new Vector2(speed, 0f);
    }

    private void Attack()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(niamh.DashCheckTransform.position, niamh.DashBoxSize, 0f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            IAttackable attackable = hit.collider.GetComponent<IAttackable>();

            if (attackable != null && !alreadyAttacked.Contains(attackable))
            {
                attackable.TakeDamage(new Damage(niamh.gameObject, niamh.DashDamage, Vector2.zero));
                alreadyAttacked.Add(attackable);
            }
        }
    }
}
