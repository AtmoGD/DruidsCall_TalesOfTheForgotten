using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class DirectionComponent : MonoBehaviour
{
    private Character character;
    [field: SerializeField] public Vector2 Direction { get; private set; } = Vector2.right;
    [field: SerializeField] public bool Invert { get; private set; } = false;

    private void Start()
    {
        character = GetComponent<Character>();

    }

    private void Update()
    {
        UpdateDirection();

        UpdateAnimationTime();
    }

    private void UpdateDirection()
    {
        Direction = new Vector2(character.Rigidbody.velocity.x < 0f ? -1f : 1f, 1f);
        Direction *= Invert ? -1f : 1f;

        if (Mathf.Abs(character.Rigidbody.velocity.x) > 0.15f)
            character.SkinHolder.localScale = new Vector3(Direction.x, 1f, 1f);
    }

    private void UpdateAnimationTime()
    {
        character.Animator.SetFloat("SpeedX", Mathf.Abs(character.Rigidbody.velocity.x) / character.MaxSpeed);
    }
}
