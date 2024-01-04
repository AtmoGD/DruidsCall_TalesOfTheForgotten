using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [field: SerializeField] public UnityEvent OnTrigger { get; set; } = new UnityEvent();

    public virtual void TriggerEvent()
    {
        OnTrigger.Invoke();
    }
}
