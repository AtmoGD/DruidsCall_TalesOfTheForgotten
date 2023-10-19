using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [field: SerializeField] public CinemachineBlendDefinition StartBlend { get; private set; } = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);

    private void Start()
    {
        CinemachineBlendManager.SetNextBlend(StartBlend);
    }
}
