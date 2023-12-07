using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    [field: SerializeField] private List<IInteractable> Interactables { get; set; } = new List<IInteractable>();

    public void Interact()
    {
        if (Interactables.Count > 0)
            Interactables[0].Interact();

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
