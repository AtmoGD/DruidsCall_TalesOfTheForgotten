using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [field: SerializeField] public List<Level> Neighbours { get; private set; }
    [field: SerializeField] public Cinemachine.CinemachineVirtualCamera VirtualCamera { get; private set; }
    [field: SerializeField] public Cinemachine.CinemachineTargetGroup TargetGroup { get; private set; }

    public void Preload()
    {
        UpdateCameraFollow();
        gameObject.SetActive(true);
        VirtualCamera.Priority = 0;
    }

    public void Activate()
    {
        UpdateCameraFollow();
        gameObject.SetActive(true);
        VirtualCamera.Priority = 10;
    }

    public void Deactivate()
    {
        VirtualCamera.Priority = 0;
        gameObject.SetActive(false);
    }

    private void UpdateCameraFollow()
    {
        VirtualCamera.Follow = Game.Manager.Niamh.transform;
        VirtualCamera.LookAt = Game.Manager.Niamh.transform;
    }
}
