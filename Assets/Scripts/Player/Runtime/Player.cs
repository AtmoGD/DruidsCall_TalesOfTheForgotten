using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerData Data { get; private set; } = new PlayerData();

    public void SaveData()
    {
        SaveSystem.SaveData(Data, Data.FileName);
    }

    public void LoadData()
    {
        Data = SaveSystem.LoadData<PlayerData>(Data.FileName);
    }
}
