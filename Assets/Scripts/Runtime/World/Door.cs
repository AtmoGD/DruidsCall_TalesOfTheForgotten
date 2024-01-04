using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [field: SerializeField] public bool IsOpen { get; set; } = false;

    [field: SerializeField] public Vector2 OpenPosition { get; set; } = Vector2.zero;
    [field: SerializeField] public float OpenSpeed { get; set; } = 1f;

    private Vector2 _closedPosition = Vector2.zero;

    private void Awake()
    {
        _closedPosition = transform.position;
    }

    private void Update()
    {
        if (IsOpen)
            transform.position = Vector2.Lerp(transform.position, _closedPosition + OpenPosition, OpenSpeed * Time.deltaTime);
        else
            transform.position = Vector2.Lerp(transform.position, _closedPosition, OpenSpeed * Time.deltaTime);
    }

    public void Open()
    {
        IsOpen = true;
    }

    public void Close()
    {
        IsOpen = false;
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + OpenPosition);
    }
}
