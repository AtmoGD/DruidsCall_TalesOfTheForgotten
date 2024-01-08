using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingEnemy : Enemy
{
    [field: SerializeField] public float Speed { get; protected set; } = 5f;
    [field: SerializeField] public List<WaypointData> Waypoints { get; protected set; } = new List<WaypointData>();
    [field: SerializeField] public bool RandomizeWaypoints { get; protected set; } = false;
    [field: SerializeField] public bool PingPongWaypoints { get; protected set; } = true;
    [field: SerializeField] public float TargetThreshold { get; protected set; } = 0.1f;

    public new PatrolingEnemyGetHit GetHitState { get; protected set; } = null;
    public EnemyPatroling PatrolingState { get; protected set; } = null;

    public Vector2 InitialPosition { get; protected set; } = Vector2.zero;

    public override void Awake()
    {
        base.Awake();

        InitialPosition = transform.position;

        GetHitState = new PatrolingEnemyGetHit(this);
        PatrolingState = new EnemyPatroling(this);
    }

    public override void Start()
    {
        ChangeState(PatrolingState);
    }

    // Completly insane that i have to do this.
    // This OnGetHit is the same as the one in Enemy.cs
    // I have to do this because otherwise it wouldn't use the child class PatrolingEnemyGetHit
    public override void OnGetHit(Damage damage)
    {
        GetHitState.Damage = damage;
        ChangeState(GetHitState);
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector2 startPos = transform.position;
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
            startPos = InitialPosition;
#endif

        foreach (var waypoint in Waypoints)
            Gizmos.DrawWireSphere(startPos + waypoint.Position, 0.3f);

#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(PatrolingState.TargetPosition, 0.3f);
        }
#endif
    }
}
