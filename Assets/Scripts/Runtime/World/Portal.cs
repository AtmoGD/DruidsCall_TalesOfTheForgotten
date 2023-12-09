using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Portal : SavePoint
{
    [field: SerializeField] public Portal OtherPortal { get; private set; } = null;
    [field: SerializeField] public MMFeedbacks TeleportFeedbacks { get; private set; } = null;

    public override void Interact()
    {
        TeleportFeedbacks?.PlayFeedbacks();

        OtherPortal.SaveGame();

        // Teleport();
    }

    public void Teleport()
    {
        Game.Manager.InitWorld();
    }
}
