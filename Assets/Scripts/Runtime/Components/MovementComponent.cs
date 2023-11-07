using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.zero;
    [field: SerializeField] public bool CanMoveHorizontal { get; set; } = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!CanMoveHorizontal) return;

        rb.velocity = moveDirection * Time.deltaTime;
    }

    public void AddMovement(Vector2 _movement)
    {
        moveDirection += _movement;
    }

    public void SetMovement(Vector2 _movement)
    {
        moveDirection = _movement;
    }
}
