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
    [field: SerializeField] public List<WaypointData> Waypoints { get; private set; } = new List<WaypointData>();
    [field: SerializeField] public bool Started { get; private set; } = false;
    [field: SerializeField] public bool Loop { get; private set; } = true;
    [field: SerializeField] public float Speed { get; private set; } = 1f;
    [field: SerializeField] public bool MoveCharacter { get; private set; } = true;

    private int currentWaypointIndex = 0;
    private WaypointData CurrentWaypoint => Waypoints[currentWaypointIndex];
    private Vector2 TargetPosition => initialPosition + CurrentWaypoint.Position;

    private float waitTimer = 0f;
    private Vector2 initialPosition = Vector2.zero;
    private List<Character> charactersOnPlatform = new List<Character>();
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

        if (MoveCharacter) MoveCharactersOnPlatform();
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
            currentWaypointIndex++;

            if (currentWaypointIndex >= Waypoints.Count)
            {
                if (Loop)
                    currentWaypointIndex = 0;
                else
                    Started = false;
            }
        }
    }

    private void MoveCharactersOnPlatform()
    {
        foreach (Character character in charactersOnPlatform)
            character.transform.position += (Vector3)LastMovement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.GetComponent<Character>();

        if (character != null)
            charactersOnPlatform.Add(character);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Character character = collision.GetComponent<Character>();

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
        }

    }
#endif
}
