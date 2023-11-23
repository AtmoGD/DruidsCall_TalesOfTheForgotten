using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DirectionComponent : MonoBehaviour
{
    [field: SerializeField] public Transform Target { get; private set; }
    [field: SerializeField] public bool Invert { get; private set; } = false;

    public Vector2 Direction { get; private set; } = Vector2.right;
    public Rigidbody2D RB { get; private set; }

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        if (Mathf.Abs(RB.velocity.x) > 0.15f)
        {
            Direction = new Vector2(RB.velocity.x < 0f ? -1f : 1f, 1f);
            Direction *= Invert ? -1f : 1f;
            Target.localScale = new Vector3(Direction.x, 1f, 1f);
        }
    }
}
