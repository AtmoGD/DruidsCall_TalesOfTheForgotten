using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlow : MonoBehaviour
{
    [field: SerializeField] public Vector2 Direction { get; private set; } = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        Niamh niamh = _other.GetComponent<Niamh>();
        if (niamh != null)
            niamh.Gliding.airFlowDirection = Direction;
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        Niamh niamh = _other.GetComponent<Niamh>();
        if (niamh != null)
            niamh.Gliding.airFlowDirection = Vector2.zero;
    }
}
