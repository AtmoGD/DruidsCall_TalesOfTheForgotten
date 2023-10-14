using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public InputController InputController { get; private set; }
    [field: SerializeField] public PlayerData Data { get; private set; } = new PlayerData();
    [field: SerializeField] public Character Character { get; private set; } = null;

    [field: SerializeField] public bool IsAactive { get; private set; } = false;

    private void Update()
    {
        if (InputController && Character && IsAactive) Character.CurrentInput = InputController.Data;
    }

    public void SaveData()
    {
        SaveSystem.SaveData(Data, Data.FileName);
    }

    public void LoadData()
    {
        Data = SaveSystem.LoadData<PlayerData>(Data.FileName);
    }
}
