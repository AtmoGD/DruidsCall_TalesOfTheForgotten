using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroling : EnemyState
{
    protected PatrolingEnemy PatrolingEnemy => Enemy as PatrolingEnemy;

    private int currentWaypointIndex = 0;
    private int indexDir = 1;
    private float waitTimer = 0f;

    public WaypointData CurrentWaypoint => PatrolingEnemy.Waypoints[currentWaypointIndex];
    public Vector2 TargetPosition => PatrolingEnemy.InitialPosition + CurrentWaypoint.Position;

    public EnemyPatroling(Enemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // currentWaypointIndex = 0;
        // indexDir = 1;
        // waitTimer = 0f;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (waitTimer > 0f)
            waitTimer -= Time.deltaTime;

        if (Enemy.Rigidbody2D.velocity.magnitude > 0.1f)
            Enemy.Animator.SetBool("Moving", true);
        else
            Enemy.Animator.SetBool("Moving", false);

        UpdateWaypointIndex();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (waitTimer > 0f)
            return;

        MoveTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        Vector2 dir = TargetPosition - (Vector2)Enemy.transform.position;
        Enemy.Rigidbody2D.velocity = dir.normalized * PatrolingEnemy.Speed;
        // Enemy.Rigidbody2D.MovePosition(Vector2.MoveTowards(Enemy.transform.position, TargetPosition, PatrolingEnemy.Speed * Time.deltaTime));
    }

    private void UpdateWaypointIndex()
    {
        if (Vector2.Distance(Enemy.transform.position, TargetPosition) < PatrolingEnemy.TargetThreshold)
        {
            waitTimer = CurrentWaypoint.WaitTime;

            if (PatrolingEnemy.RandomizeWaypoints)
            {
                currentWaypointIndex = Random.Range(0, PatrolingEnemy.Waypoints.Count);
            }
            else
            {
                currentWaypointIndex += indexDir;

                if (currentWaypointIndex >= PatrolingEnemy.Waypoints.Count || currentWaypointIndex < 0)
                {
                    if (PatrolingEnemy.PingPongWaypoints)
                    {

                        indexDir *= -1;
                        currentWaypointIndex += indexDir;
                    }
                    else
                        currentWaypointIndex = 0;
                }
            }

            MoveTowardsWaypoint();
        }
    }
}
