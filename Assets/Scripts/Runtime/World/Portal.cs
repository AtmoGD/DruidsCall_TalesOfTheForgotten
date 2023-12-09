using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Portal : SavePoint
{
    [field: SerializeField] public Portal OtherPortal { get; private set; } = null;

    public override void Interact()
    {
        SaveFeedbacks?.PlayFeedbacks();

        OtherPortal.SaveGame();

        Teleport();
    }

    public void Teleport()
    {
        Game.Manager.InitWorld();
    }
}
