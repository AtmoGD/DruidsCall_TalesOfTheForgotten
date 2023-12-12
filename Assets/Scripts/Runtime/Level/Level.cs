using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Pathfinding;

public class Level : MonoBehaviour
{
    [field: SerializeField] public List<Level> Neighbours { get; private set; }
    [field: SerializeField] public Cinemachine.CinemachineVirtualCamera VirtualCamera { get; private set; }
    [field: SerializeField] public Cinemachine.CinemachineTargetGroup TargetGroup { get; private set; }
    [field: SerializeField] public float NiamhWeight { get; private set; } = 1f;
    [field: SerializeField] public float NiamhRadius { get; private set; } = 8f;

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

        if (Game.Manager.Niamh && Game.Manager.Niamh.IsActive)
            TargetGroup.AddMember(Game.Manager.Niamh.transform, NiamhWeight, NiamhRadius);
    }
}
