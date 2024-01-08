using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Button : MonoBehaviour
{
    [field: SerializeField] public UnityEvent OnPress { get; private set; } = new UnityEvent();
    [field: SerializeField] public UnityEvent OnRelease { get; private set; } = new UnityEvent();
    [field: SerializeField] public LayerMask TriggerLayer { get; private set; } = 0;

    public bool IsPressed { get; private set; } = false;

    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((TriggerLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            IsPressed = true;
            OnPress?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((TriggerLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            IsPressed = false;
            OnRelease?.Invoke();
        }
    }

    public void SetBoolTrue(string name)
    {
        animator.SetBool(name, true);
    }

    public void SetBoolFalse(string name)
    {
        animator.SetBool(name, false);
    }
}
