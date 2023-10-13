using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerData Data { get; private set; } = new PlayerData();

    private void Start()
    {
        SaveData();
        LoadData();
    }

    public void SaveData()
    {
        SaveSystem.SaveData(Data, Data.Path);
    }

    public void LoadData()
    {
        Data = SaveSystem.LoadData<PlayerData>(Data.Path);
    }
}
