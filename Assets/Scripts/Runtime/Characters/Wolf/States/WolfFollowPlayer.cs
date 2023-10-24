using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFollowPlayer : WolfMoving
{
    public WolfFollowPlayer(Wolf _wolf) : base(_wolf) { }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entering FollowPlayer State");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        FollowCharacter();
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

        Debug.Log("Exiting FollowPlayer State");
    }

    private void FollowCharacter()
    {
        float x = wolf.Hero.transform.position.x - wolf.transform.position.x;
        x = Utils.Remap01(x, wolf.FollowRadius, wolf.TeleportRadius);
        wolf.CurrentInput.Move = new Vector2(x, 0f);
    }
}
