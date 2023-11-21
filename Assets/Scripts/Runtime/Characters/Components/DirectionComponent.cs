using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class DirectionComponent : MonoBehaviour
{
    private Character character;
    [field: SerializeField] public Vector2 Direction { get; private set; } = Vector2.right;

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
        if (Mathf.Abs(character.Rigidbody.velocity.x) > 0.15f)
            character.transform.localScale = new Vector3(character.Rigidbody.velocity.x < 0f ? -1f : 1f, 1f, 1f);
    }

    private void UpdateAnimationTime()
    {
        character.Animator.SetFloat("SpeedX", Mathf.Abs(character.Rigidbody.velocity.x) / character.MaxSpeed);
    }
}
