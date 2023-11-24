using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinnAttacking : FinnState
{
    private int dir = 1;
    public FinnAttacking(Finn _finn) : base(_finn)
    {
    }

    public override void Enter()
    {
        base.Enter();

        dir = finn.Hero.DirectionComponent.Direction.x > 0 ? 1 : -1;

        // finn.Animator.SetBool("Attacking", true);
        finn.Animator.SetTrigger("Attack");

        finn.AttackFeedbacks.PlayFeedbacks();

        // finn.SkinHolder.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();


        finn.transform.position += Vector3.right * dir * Time.deltaTime * finn.AttackMoveSpeed;

        // Vector2 newPos = Vector2.Lerp(finn.transform.position, finn.Hero.AttackPoint.position, Time.deltaTime * finn.Speed);
        // finn.transform.position = newPos;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (timeInState > finn.AttackMoveTime)
            finn.ChangeState(finn.Following);
    }

    public override void Exit()
    {
        base.Exit();
        // finn.SkinHolder.rotation = Quaternion.identity;

        // finn.Animator.SetBool("Attacking", false);
    }
}
