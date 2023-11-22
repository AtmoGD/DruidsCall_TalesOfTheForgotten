using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheckComponent : MonoBehaviour
{
    [field: SerializeField] public bool IsActive { get; private set; } = true;

    [field: Header("Obstacle Checking")]
    [field: SerializeField] public LayerMask ObstacleCheckLayer { get; private set; } = 0;
    [field: SerializeField] public Transform MiddleLeftCheck { get; private set; } = null;
    [field: SerializeField] public Transform MiddleRightCheck { get; private set; } = null;
    [field: SerializeField] public Transform UpLeftCheck { get; private set; } = null;
    [field: SerializeField] public Transform UpRightCheck { get; private set; } = null;

    [field: SerializeField] public float CheckRadius { get; private set; } = 0.1f;

    [field: SerializeField] public bool IsObstacleMiddleLeft { get; private set; } = false;
    [field: SerializeField] public bool IsObstacleMiddleRight { get; private set; } = false;
    [field: SerializeField] public bool IsObstacleUpLeft { get; private set; } = false;
    [field: SerializeField] public bool IsObstacleUpRight { get; private set; } = false;

    private void Update()
    {
        if (!IsActive) return;

        IsObstacleMiddleLeft = Physics2D.OverlapCircle(MiddleLeftCheck.position, CheckRadius, ObstacleCheckLayer);
        IsObstacleMiddleRight = Physics2D.OverlapCircle(MiddleRightCheck.position, CheckRadius, ObstacleCheckLayer);
        IsObstacleUpLeft = Physics2D.OverlapCircle(UpLeftCheck.position, CheckRadius, ObstacleCheckLayer);
        IsObstacleUpRight = Physics2D.OverlapCircle(UpRightCheck.position, CheckRadius, ObstacleCheckLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (!IsActive) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(MiddleLeftCheck.position, CheckRadius);
        Gizmos.DrawWireSphere(MiddleRightCheck.position, CheckRadius);
        Gizmos.DrawWireSphere(UpLeftCheck.position, CheckRadius);
        Gizmos.DrawWireSphere(UpRightCheck.position, CheckRadius);
    }
}
