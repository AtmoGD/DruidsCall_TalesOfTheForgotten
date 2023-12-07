using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum GameWorld
{
    Ordinary,
    Other
}

[Serializable]
public class SavePointData
{
    [field: SerializeField] public GameWorld World { get; set; } = GameWorld.Ordinary;
    [field: SerializeField] public string SavePointName { get; set; } = "SavePoint";
}

public class SavePoint : MonoBehaviour, IInteractable
{
    [field: SerializeField] public SavePointData Data { get; private set; } = new SavePointData();

    public void Interact()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        Game.Manager.Data.CurrentSavePoint = Data;
        Game.Manager.SaveGame();

#if UNITY_EDITOR
        print("Game Saved!");
#endif
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<InteractionComponent>() != null)
        {
            other.GetComponent<InteractionComponent>().AddInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<InteractionComponent>() != null)
        {
            other.GetComponent<InteractionComponent>().RemoveInteractable(this);
        }
    }
}
