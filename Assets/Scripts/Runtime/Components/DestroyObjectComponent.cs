using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [field: SerializeField] public bool DestroyOnStart { get; private set; } = false;
    [field: SerializeField] public float DestroyTime { get; private set; } = 1f;

    private void Start()
    {
        if (DestroyOnStart)
            DestroyObject(DestroyTime);
    }

    public void DestroyObject(float time)
    {
        Destroy(gameObject, time);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
