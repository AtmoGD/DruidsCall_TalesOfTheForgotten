using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Entrance : MonoBehaviour
{
    [field: SerializeField] public Level Level { get; private set; }
    [field: SerializeField] public bool OverrideBlend { get; private set; } = false;
    [field: SerializeField] public CinemachineBlendDefinition BlendOverride { get; private set; } = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);

    private void Start()
    {
        if (Level == null)
            Debug.LogError("Level not set in Entrance script");
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        MainCharacter mainCharacter = _other.GetComponent<MainCharacter>();

        if (mainCharacter != null)
        {
            Game.Manager.World.ActivateLevel(Level);

            if (OverrideBlend) CinemachineBlendManager.SetNextBlend(BlendOverride);
        }
    }
}
