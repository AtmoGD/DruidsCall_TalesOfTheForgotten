using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [field: SerializeField] public World ActiveWorld { get; set; } = null;
    [field: SerializeField] public World OrdinaryWorld { get; set; } = null;
    [field: SerializeField] public World OtherWorld { get; set; } = null;

    public void ChangeActiveWorld(GameWorld _world)
    {
        switch (_world)
        {
            case GameWorld.Ordinary:
                ActiveWorld = OrdinaryWorld;
                OrdinaryWorld.gameObject.SetActive(true);
                OtherWorld.gameObject.SetActive(false);
                break;
            case GameWorld.Other:
                ActiveWorld = OtherWorld;
                OrdinaryWorld.gameObject.SetActive(false);
                OtherWorld.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void OpenOrdinaryWorld()
    {
        ChangeActiveWorld(GameWorld.Ordinary);
    }

    public void OpenOtherWorld()
    {
        ChangeActiveWorld(GameWorld.Other);
    }
}
