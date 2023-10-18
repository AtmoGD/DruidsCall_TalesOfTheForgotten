using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public InputController InputController { get; private set; }
    // [field: SerializeField] public PlayerData Data { get; private set; } = new PlayerData();
    [field: SerializeField] public Character Character { get; private set; } = null;
    [field: SerializeField] public MainCharacter MainCharacter { get; private set; } = null;
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;

    [field: SerializeField] public bool IsActive { get; private set; } = false;

    private void Start()
    {
        SwitchCharacter();
        SwitchCharacter();

        // LoadData();
    }

    public void SwitchCharacter()
    {
        if (Character == MainCharacter)
        {
            Character = Wolf;
            Wolf.CurrentInput = InputController.Data;
            MainCharacter.CurrentInput = new InputData();
        }
        else
        {
            Character = MainCharacter;
            MainCharacter.CurrentInput = InputController.Data;
            Wolf.CurrentInput = new InputData();
        }
    }

    // public void SaveData()
    // {
    //     // SaveSystem.SaveData(Data, Data.FileName);
    // }

    // public void LoadData()
    // {
    //     // Data = SaveSystem.LoadData<PlayerData>(Data.FileName);
    // }
}
