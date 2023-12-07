using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinnAttacking : FinnState
{
    private int dir = 1;
    public FinnAttacking(Finn _finn) : base(_finn) { }

    public override void Enter()
    {
        base.Enter();

        dir = finn.Hero.DirectionComponent.Direction.x > 0 ? 1 : -1;

        finn.Animator.SetTrigger("Attack");

        finn.AttackFeedbacks.PlayFeedbacks();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        finn.transform.position = Vector2.Lerp(finn.transform.position, finn.Hero.AttackEndPoint.position, Time.deltaTime * finn.AttackSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (timeInState > finn.AttackTime)
            finn.ChangeState(finn.Following);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
