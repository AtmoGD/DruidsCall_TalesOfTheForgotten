using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinnFollowing : FinnState
{
    Vector2 direction;
    float idleTimer = 0f;

    public FinnFollowing(Finn _finn) : base(_finn)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        UpdateMovement();

        UpdateAnimation();

        if (direction.magnitude < 0.1f)
            idleTimer += Time.deltaTime;
        else
            idleTimer = 0f;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (idleTimer > finn.IdleTime)
            finn.ChangeState(finn.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }

    void UpdateMovement()
    {
        direction = finn.transform.position - finn.Hero.Follow.position;

        Vector2 newPos = Vector2.Lerp(finn.transform.position, finn.Hero.Follow.position, Time.deltaTime * finn.Speed);
        finn.transform.position = newPos;

        if (Mathf.Abs(finn.Hero.Rigidbody.velocity.x) > 0.15f)
            finn.SkinHolder.localScale = finn.Hero.DirectionComponent.Direction;
    }

    void UpdateAnimation()
    {
        finn.Animator.SetFloat("SpeedX", direction.x * 2);
        finn.Animator.SetFloat("SpeedY", direction.y * 2);
    }
}
