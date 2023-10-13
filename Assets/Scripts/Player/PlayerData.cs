using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] public string Path = "Test/EditorSave.druid";
    [SerializeField] public float PlayTime = 0f;
    [SerializeField] public bool IsNewGame = true;
}
