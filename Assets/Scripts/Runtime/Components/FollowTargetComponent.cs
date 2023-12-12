using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Pathfinding;

public class FollowTargetComponent : MonoBehaviour
{
    // [field: SerializeField] public Transform Target { get; set; }
    // [field: SerializeField] public float UpdateInterval { get; set; } = 0.5f;

    // [field: SerializeField] public Path Path { get; private set; }
    // [field: SerializeField] public int CurrentWaypoint { get; set; } = 0;
    // [field: SerializeField] public Seeker Seeker { get; private set; }

    // public Vector2 Direction
    // {
    //     get
    //     {
    //         if (Path == null) return Vector2.zero;
    //         if (CurrentWaypoint >= Path.vectorPath.Count) return Vector2.zero;

    //         Vector2 direction = ((Vector2)Path.vectorPath[CurrentWaypoint] - (Vector2)transform.position).normalized;
    //         return direction;
    //     }
    // }

    // private void UpdatePath()
    // {
    //     if (Seeker.IsDone())
    //         Seeker.StartPath(transform.position, Target.position, OnPathComplete);
    // }

    // private void OnPathComplete(Path p)
    // {
    //     if (!p.error)
    //     {
    //         Path = p;
    //         CurrentWaypoint = 0;
    //     }
    // }
}
