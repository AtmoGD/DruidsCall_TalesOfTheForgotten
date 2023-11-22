using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheckComponent : MonoBehaviour
{
    [field: SerializeField] public bool IsActive { get; private set; } = true;

    [field: Header("Obstacle Checking")]
    [field: SerializeField] public LayerMask ObstacleCheckLayer { get; private set; } = 0;

    [field: SerializeField] public Transform MiddleLeftCheck { get; private set; } = null;
    [field: SerializeField] public Vector2 MiddleLeftCheckSize { get; private set; } = Vector2.zero;
    [field: SerializeField] public Transform MiddleRightCheck { get; private set; } = null;
    [field: SerializeField] public Vector2 MiddleRightCheckSize { get; private set; } = Vector2.zero;
    [field: SerializeField] public Transform MiddleUpLeftCheck { get; private set; } = null;
    [field: SerializeField] public Vector2 MiddleUpLeftCheckSize { get; private set; } = Vector2.zero;
    [field: SerializeField] public Transform MiddleUpRightCheck { get; private set; } = null;
    [field: SerializeField] public Vector2 MiddleUpRightCheckSize { get; private set; } = Vector2.zero;
    [field: SerializeField] public Transform UpLeftCheck { get; private set; } = null;
    [field: SerializeField] public Vector2 UpLeftCheckSize { get; private set; } = Vector2.zero;
    [field: SerializeField] public Transform UpRightCheck { get; private set; } = null;
    [field: SerializeField] public Vector2 UpRightCheckSize { get; private set; } = Vector2.zero;

    [field: SerializeField] public bool ObstacleMiddleLeft { get; private set; } = false;
    [field: SerializeField] public bool ObstacleMiddleRight { get; private set; } = false;
    [field: SerializeField] public bool ObstacleMiddleUpLeft { get; private set; } = false;
    [field: SerializeField] public bool ObstacleMiddleUpRight { get; private set; } = false;
    [field: SerializeField] public bool ObstacleUpLeft { get; private set; } = false;
    [field: SerializeField] public bool ObstacleUpRight { get; private set; } = false;

    private void Update()
    {
        if (!IsActive) return;

        ObstacleMiddleLeft = Physics2D.OverlapBox(MiddleLeftCheck.position, MiddleLeftCheckSize, 0, ObstacleCheckLayer);
        ObstacleMiddleRight = Physics2D.OverlapBox(MiddleRightCheck.position, MiddleRightCheckSize, 0, ObstacleCheckLayer);
        ObstacleMiddleUpLeft = Physics2D.OverlapBox(MiddleUpLeftCheck.position, MiddleUpLeftCheckSize, 0, ObstacleCheckLayer);
        ObstacleMiddleUpRight = Physics2D.OverlapBox(MiddleUpRightCheck.position, MiddleUpRightCheckSize, 0, ObstacleCheckLayer);
        ObstacleUpLeft = Physics2D.OverlapBox(UpLeftCheck.position, UpLeftCheckSize, 0, ObstacleCheckLayer);
        ObstacleUpRight = Physics2D.OverlapBox(UpRightCheck.position, UpRightCheckSize, 0, ObstacleCheckLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (!IsActive) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(MiddleLeftCheck.position, MiddleLeftCheckSize);
        Gizmos.DrawWireCube(MiddleRightCheck.position, MiddleRightCheckSize);
        Gizmos.DrawWireCube(MiddleUpLeftCheck.position, MiddleUpLeftCheckSize);
        Gizmos.DrawWireCube(MiddleUpRightCheck.position, MiddleUpRightCheckSize);
        Gizmos.DrawWireCube(UpLeftCheck.position, UpLeftCheckSize);
        Gizmos.DrawWireCube(UpRightCheck.position, UpRightCheckSize);
    }
}
