using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfTeleportToHero : WolfState
{
    public WolfTeleportToHero(Wolf _wolf, string _animationName = "TeleportToHero") : base(_wolf, _animationName) { }

    public override void Enter()
    {
        base.Enter();

        wolf.Rigidbody.velocity = Vector2.zero;

        wolf.transform.position = wolf.FollowTransform.position + (Vector3)wolf.TeleportOffset;

        wolf.CurrentInput.TeleportToHero = false;

        wolf.ChangeState(wolf.Idle);

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Entering TeleportToHero State");
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
    }

    public override void Exit()
    {
        base.Exit();

        if (wolf.ShowDebugLogs)
            Debug.Log("Wolf: Exiting TeleportToHero State");
    }
}
