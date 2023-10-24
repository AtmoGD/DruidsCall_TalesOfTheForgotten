using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Manager { get; private set; }
    public bool SetDontDestroyOnLoad { get; private set; } = true;

    [field: Header("Game References")]
    [field: SerializeField] public Player Player { get; private set; } = null;
    [field: SerializeField] public Hero MainCharacter { get; private set; } = null;
    [field: SerializeField] public Wolf Wolf { get; private set; } = null;
    [field: SerializeField] public UIController UIController { get; private set; } = null;
    [field: SerializeField] public World World { get; private set; } = null;

    [field: Header("Game Settings")]
    [field: SerializeField] public string CurrentActiveFileName { get; private set; } = "GameData";
    [field: SerializeField] public GameData Data { get; private set; } = new GameData();

    private void Awake()
    {
        if (Manager) { Destroy(gameObject); return; }

        Manager = this;

        if (SetDontDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadGame(CurrentActiveFileName);
    }

    public void LoadGame(string _fileName)
    {
        CurrentActiveFileName = _fileName;
    }

    public void SaveGame()
    {
        SaveSystem.SaveData(Data, CurrentActiveFileName);
    }
}
