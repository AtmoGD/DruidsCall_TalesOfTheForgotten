using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public InputController InputController { get; private set; }
    [field: SerializeField] public PlayerData Data { get; private set; } = new PlayerData();
    [field: SerializeField] public Character Character { get; private set; }

    public void SaveData()
    {
        SaveSystem.SaveData(Data, Data.FileName);
    }

    public void LoadData()
    {
        Data = SaveSystem.LoadData<PlayerData>(Data.FileName);
    }
}
