using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public Character Character { get; private set; } = null;
    [field: SerializeField] public PlayerInputController PlayerInputController { get; private set; }
    [field: SerializeField] public Hero Hero { get; private set; } = null;
    [field: SerializeField] public WolfInputController WolfInputController { get; private set; } = null;
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;

    [field: SerializeField] public bool IsActive { get; private set; } = false;

    private void Start()
    {
        Hero.CurrentInput = PlayerInputController.HeroInput;
        Wolf.CurrentInput = WolfInputController.WolfInput;
        // SwitchCharacter();
        // SwitchCharacter();

        // LoadData();
    }

    public void SwitchCharacter()
    {
        if (Character == Hero)
        {
            Character = Wolf;
            Wolf.CurrentInput = PlayerInputController.WolfInput;
            Wolf.SetIsControlledByPlayer(true);
            Hero.SetIsControlledByPlayer(false);
            Hero.CurrentInput = new HeroInput();
        }
        else
        {
            Character = Hero;
            Hero.CurrentInput = PlayerInputController.HeroInput;
            Hero.SetIsControlledByPlayer(true);
            Wolf.SetIsControlledByPlayer(false);
            Wolf.CurrentInput = WolfInputController.WolfInput;
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
