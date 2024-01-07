using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Manager { get; private set; }
    public bool SetDontDestroyOnLoad { get; private set; } = true;

    [field: Header("Game References")]
    [field: SerializeField] public Player Player { get; private set; } = null;
    [field: SerializeField] public Niamh Niamh { get; private set; } = null;
    [field: SerializeField] public Finn Finn { get; private set; } = null;
    [field: SerializeField] public Camera UICamera { get; private set; } = null;
    [field: SerializeField] public SavePoint StartPoint { get; private set; } = null;
    [field: SerializeField] public UIController UIController { get; private set; } = null;
    [field: SerializeField] public WorldController WorldController { get; private set; } = null;

    [field: Header("Game Settings")]
    [field: SerializeField] public string CurrentActiveFileName { get; private set; } = "GameData";
    [field: SerializeField] public bool AutoRestartOnDeath { get; private set; } = true;
    [field: SerializeField] public GameData Data { get; private set; } = new GameData();

    [field: SerializeField] public List<SavePoint> SavePoints { get; private set; } = new List<SavePoint>();

    private void Awake()
    {
        if (Manager) { Destroy(gameObject); return; }

        Manager = this;

        if (SetDontDestroyOnLoad) DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SavePoints = new List<SavePoint>(GameObject.FindObjectsOfType<SavePoint>());

        LoadGame(CurrentActiveFileName);
    }

    public void LoadGame(string _fileName)
    {
        CurrentActiveFileName = _fileName;
        Data = SaveSystem.LoadData<GameData>(CurrentActiveFileName);

        Data ??= new GameData // <-- Means if Data is null, then create a new GameData object
        {
            CurrentTitle = "Tutorial",
            CurrentSavePoint = StartPoint.Data
        };

        InitWorld();
    }

    public void InitWorld()
    {
        SavePoint savePoint = SavePoints.Find(x => x.Data.SavePointName == Data.CurrentSavePoint.SavePointName);

        WorldController.ChangeActiveWorld(Data.CurrentSavePoint.World);

        Level level = WorldController.ActiveWorld.Levels.Find(x => x.gameObject.name == Data.CurrentSavePoint.LevelName);
        WorldController.ActiveWorld.ActivateLevel(level);


        if (savePoint)
        {
            Niamh.transform.position = savePoint.transform.position;
            Niamh.Init();

            Finn.transform.position = savePoint.transform.position;
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveData<GameData>(Data, CurrentActiveFileName);
    }

    public void FindSavePoints()
    {
        SavePoints = new List<SavePoint>(GameObject.FindObjectsOfType<SavePoint>());
    }

    public void InitializeSavePoints(bool _forceNewId = false)
    {
        SavePoints = new List<SavePoint>(GameObject.FindObjectsOfType<SavePoint>());

        foreach (SavePoint savePoint in SavePoints)
        {
            savePoint.Init(_forceNewId);
        }
    }
}
