using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMOD;

[Serializable]
public class WaypointData
{
    public Vector2 Position;
    public float WaitTime;
}

public class MovingPlatform : MonoBehaviour
{
    [field: Tooltip("If true, the platform will start moving when the scene starts. If false, the platform will not start moving until Started is set to true.")]
    [field: SerializeField] public bool Started { get; private set; } = false;
    [field: Tooltip("If true, the platform will continue to move through the waypoints in order. If false, the platform will stop moving when it reaches the end of the path.")]
    [field: SerializeField] public bool Loop { get; private set; } = true;
    [field: Tooltip("If true, the platform will move the waypoints in reverse order when it reaches the end of the path. If false, the platform will move to the first waypoint when it reaches the end of the path.")]
    [field: SerializeField] public bool PingPong { get; private set; } = false;
    [field: Tooltip("If true, the platform will move any rigidbodys that are on the platform.")]
    [field: SerializeField] public bool MoveBodysOnPlatform { get; private set; } = true;
    [field: SerializeField] public float Speed { get; private set; } = 1f;
    [field: SerializeField] public List<WaypointData> Waypoints { get; private set; } = new List<WaypointData>();

    private int currentWaypointIndex = 0;
    private int indexDir = 1;
    private WaypointData CurrentWaypoint => Waypoints[currentWaypointIndex];
    private Vector2 TargetPosition => initialPosition + CurrentWaypoint.Position;

    private float waitTimer = 0f;
    private Vector2 initialPosition = Vector2.zero;
    private List<Rigidbody2D> charactersOnPlatform = new();
    private Vector2 LastMovement = Vector2.zero;

    private void Start()
    {
        initialPosition = transform.position;
        transform.position = initialPosition + CurrentWaypoint.Position;
    }

    private void Update()
    {
        if (!Started)
            return;

        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        MoveTowardsWaypoint();
        UpdateWaypointIndex();

        if (MoveBodysOnPlatform) MoveRigidbodysOnPlatform();
    }

    private void MoveTowardsWaypoint()
    {
        Vector2 startPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
        LastMovement = (Vector2)transform.position - startPosition;
    }

    private void UpdateWaypointIndex()
    {
        if (Vector2.Distance(transform.position, TargetPosition) < 0.01f)
        {
            waitTimer = CurrentWaypoint.WaitTime;
            currentWaypointIndex += indexDir;

            if (currentWaypointIndex >= Waypoints.Count || currentWaypointIndex < 0)
            {
                if (Loop)
                {
                    if (PingPong)
                    {

                        indexDir *= -1;
                        currentWaypointIndex += indexDir;
                    }
                    else
                        currentWaypointIndex = 0;
                }
                else
                    Started = false;
            }
        }
    }

    private void MoveRigidbodysOnPlatform()
    {
        foreach (Rigidbody2D character in charactersOnPlatform)
            character.transform.position += (Vector3)LastMovement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D character = collision.GetComponent<Rigidbody2D>();

        if (character != null)
            charactersOnPlatform.Add(character);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D character = collision.GetComponent<Rigidbody2D>();

        if (character != null)
            charactersOnPlatform.Remove(character);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector2 startPos = UnityEditor.EditorApplication.isPlaying ? initialPosition : (Vector2)transform.position;

        for (int i = 0; i < Waypoints.Count; i++)
        {
            WaypointData waypoint = Waypoints[i];
            Vector2 position = startPos + waypoint.Position;

            Gizmos.DrawSphere(position, 0.1f);

            if (i < Waypoints.Count - 1)
            {
                WaypointData nextWaypoint = Waypoints[i + 1];
                Vector2 nextPosition = startPos + nextWaypoint.Position;
                Gizmos.DrawLine(position, nextPosition);
            }
            else
            {
                if (Loop && !PingPong)
                {
                    WaypointData nextWaypoint = Waypoints[0];
                    Vector2 nextPosition = startPos + nextWaypoint.Position;
                    Gizmos.DrawLine(position, nextPosition);
                }
            }
        }

    }
#endif
}
