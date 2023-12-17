using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LayerData
{
    public Transform transform;
    public Vector2 speed;
}

public class ParallaxController : MonoBehaviour
{
    [field: SerializeField] public Transform Target { get; private set; } = null;
    [field: SerializeField] public Transform Reference { get; private set; } = null;
    [field: SerializeField] public float Speed { get; private set; } = 10f;
    [field: SerializeField] public List<LayerData> Layers { get; private set; } = new List<LayerData>();

    // private Vector2 lastPosition;

    private void Start()
    {
        // if (!Target) Target = Camera.main.transform;
        Target = Camera.main.transform;
        // Target = Game.Manager.Niamh.transform;
        if (!Reference) Reference = transform;
    }

    // private void Start()
    // {
    //     lastPosition = Target.position;
    // }

    private void FixedUpdate()
    {
        Vector2 dir = Target.position - Reference.position;

        foreach (LayerData layer in Layers)
        {
            // Vector2 newPos = new Vector2(
            //     dir.x * layer.speed.x,
            //     dir.y * layer.speed.y
            // );

            // layer.transform.localPosition = Vector2.Lerp(layer.transform.localPosition, newPos, Time.deltaTime);

            layer.transform.localPosition = dir * layer.speed;
        }

        // lastPosition = Target.position;
    }
}
