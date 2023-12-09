using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.Feedbacks;

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
    [field: SerializeField] public string SavePointName { get; set; } = "";
    [field: SerializeField] public string LevelName { get; set; } = "Level";
}

public class SavePoint : MonoBehaviour, IInteractable
{
    [field: SerializeField] public SavePointData Data { get; private set; } = new SavePointData();
    [field: SerializeField] public MMFeedbacks SaveFeedbacks { get; private set; } = null;

    public virtual void Interact()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        Game.Manager.Data.IsNewGame = false;
        Data.World = transform.GetComponentInParent<World>(true).WorldType;
        Data.LevelName = transform.GetComponentInParent<Level>(true).gameObject.name;
        Game.Manager.Data.CurrentSavePoint = Data;
        Game.Manager.SaveGame();

        SaveFeedbacks?.PlayFeedbacks();

#if UNITY_EDITOR
        print("Game Saved!");
#endif
    }

    public void Init()
    {
        World world = GetComponentInParent<World>(true);
        if (world)
            Data.World = world.WorldType;
        else
        {
            Debug.LogError("SavePointEditor: No World found in parent!");
            return;
        }

        Level level = GetComponentInParent<Level>(true);
        if (level)
            Data.LevelName = level.name;
        else
        {
            Debug.LogError("SavePointEditor: No Level found in parent!");
            return;
        }

        if (Data.SavePointName == "")
            Data.SavePointName = System.Guid.NewGuid().ToString();
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
