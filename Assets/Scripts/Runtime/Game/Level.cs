using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [field: SerializeField] public List<Level> Neighbours { get; private set; }
    [field: SerializeField] public Cinemachine.CinemachineVirtualCamera VirtualCamera { get; private set; }
    [field: SerializeField] public Cinemachine.CinemachineTargetGroup TargetGroup { get; private set; }
    [field: SerializeField] public float CharacterWeight { get; private set; } = 1f;
    [field: SerializeField] public float CharacterRadius { get; private set; } = 8f;
    [field: SerializeField] public float WolfWeight { get; private set; } = 1f;
    [field: SerializeField] public float WolfRadius { get; private set; } = 5f;

    public void Preload()
    {
        UpdateTargetGroup();
        gameObject.SetActive(true);
        VirtualCamera.Priority = 0;
    }

    public void Activate()
    {
        UpdateTargetGroup();
        gameObject.SetActive(true);
        VirtualCamera.Priority = 10;
    }

    public void Deactivate()
    {
        VirtualCamera.Priority = 0;
        gameObject.SetActive(false);
    }

    private void UpdateTargetGroup()
    {
        TargetGroup.m_Targets = new Cinemachine.CinemachineTargetGroup.Target[0];

        if (Game.Manager.MainCharacter && Game.Manager.MainCharacter.IsActive)
            TargetGroup.AddMember(Game.Manager.MainCharacter.transform, CharacterWeight, CharacterRadius);

        if (Game.Manager.Wolf && Game.Manager.Wolf.IsActive)
            TargetGroup.AddMember(Game.Manager.Wolf.transform, WolfWeight, WolfRadius);
    }
}
