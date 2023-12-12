using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinnDashing : FinnState
{
    public FinnDashing(Finn _finn) : base(_finn) { }

    public override void Enter()
    {
        base.Enter();

        finn.Animator.SetTrigger("Dash");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        UpdateMovement();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoStateChecks()
    {
        base.DoStateChecks();

        if (timeInState > finn.DashTime)
            finn.ChangeState(finn.Following);
    }

    public override void Exit()
    {
        base.Exit();
    }

    void UpdateMovement()
    {
        // Vector2 newPos = Vector2.Lerp(finn.transform.position, finn.Niamh.Follow.position, Time.deltaTime * finn.Speed);
        finn.transform.position = finn.Niamh.Follow.position;

        finn.SkinHolder.localScale = finn.Niamh.DirectionComponent.Direction;
    }
}
