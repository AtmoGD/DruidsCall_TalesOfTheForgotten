using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Cinemachine;

public class Portal : SavePoint
{
    [field: SerializeField] public Portal OtherPortal { get; private set; } = null;
    [field: SerializeField] public MMFeedbacks TeleportFeedbacks { get; private set; } = null;
    [field: SerializeField] public bool OverrideBlend { get; private set; } = true;
    [field: SerializeField] public CinemachineBlendDefinition BlendOverride { get; private set; } = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);

    public override void Interact(Niamh _niamh)
    {
        if (!_niamh.TeleportActive || _niamh.CooldownComponent.HasCooldown(_niamh.TeleportName)) return;

        _niamh.CooldownComponent.AddCooldown(new Cooldown(_niamh.TeleportName, _niamh.TeleportCooldown));

        TeleportFeedbacks?.PlayFeedbacks();

        OtherPortal.SaveGame();

        Game.Manager.UIController.PlayTeleportAnimation();
    }

    public void Teleport()
    {
        Game.Manager.InitWorld();

        if (OverrideBlend) CinemachineBlendManager.SetNextBlend(BlendOverride);
    }
}
