using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    protected Rigidbody2D rb;
    [field: SerializeField] public bool CanMove = true;
    [field: SerializeField] public float MaxSpeed = 10f;
    [field: SerializeField] public AnimationCurve Accelleration = new AnimationCurve();
    [field: SerializeField] public AnimationCurve Decceleration = new AnimationCurve();
    protected Vector2 targetVelocity = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        float xVel = _context.ReadValue<Vector2>().x;
    }
}
