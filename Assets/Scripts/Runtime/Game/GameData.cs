using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    [field: SerializeField] public bool IsNewGame { get; set; } = true;
    [field: SerializeField] public string FileName { get; private set; } = "GameData";
    [field: SerializeField] public float TimePlayed { get; set; } = 0f;
}
