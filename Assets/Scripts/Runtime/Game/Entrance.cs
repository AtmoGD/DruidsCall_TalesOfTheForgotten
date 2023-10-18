using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    [field: SerializeField] public Level Level { get; private set; }

    private void Start()
    {
        if (Level == null)
            Debug.LogError("Level not set in Entrance script");
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        MainCharacter mainCharacter = _other.GetComponent<MainCharacter>();

        if (mainCharacter != null)
            Game.Manager.World.ActivateLevel(Level);
    }
}
