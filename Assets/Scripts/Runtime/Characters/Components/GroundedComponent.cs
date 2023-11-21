using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class GroundedComponent : MonoBehaviour
{
    private Character character;
    [field: SerializeField] public float GroundedResetTime { get; private set; } = 0.5f;
    [field: SerializeField] public bool Grounded { get; private set; } = false;
    [field: SerializeField] public Vector2 LastGroundedPosition { get; private set; } = Vector2.zero;

    private float groundedResetTimer = 0f;

    private void Start()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        bool wasGrounded = Grounded;
        Grounded = Physics2D.OverlapBox(character.GroundTransform.position, character.GroundBoxSize, 0, character.GroundLayer);

        if (Grounded)
        {
            groundedResetTimer += Time.deltaTime;
            if (groundedResetTimer >= GroundedResetTime)
            {
                LastGroundedPosition = character.GroundTransform.position;
                groundedResetTimer = 0f;
            }

            if (!wasGrounded)
                character.ResetJumpsLeft();
        }
    }
}
