using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Niamh))]
public class InteractionComponent : MonoBehaviour
{
    [field: SerializeField] private List<IInteractable> Interactables { get; set; } = new List<IInteractable>();
    private Niamh Niamh { get; set; } = null;

    private void Awake()
    {
        Niamh = GetComponent<Niamh>();
    }

    public void Interact()
    {
        if (Interactables.Count > 0)
            Interactables[0].Interact(Niamh);

    }

    public void AddInteractable(IInteractable interactable)
    {
        Interactables.Add(interactable);
    }

    public void RemoveInteractable(IInteractable interactable)
    {
        Interactables.Remove(interactable);
    }
}
