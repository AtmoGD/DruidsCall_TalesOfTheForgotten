using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [field: SerializeField] public List<Level> Levels { get; private set; } = new List<Level>();
    [field: SerializeField] public Level CurrentLevelActive { get; private set; } = null;

    private void Start()
    {
        if (CurrentLevelActive)
            ActivateLevel(CurrentLevelActive);
    }

    public void ActivateLevel(Level _level)
    {
        // if (CurrentLevelActive == _level) return;

        foreach (Level level in Levels)
        {
            if (level == _level)
                level.Activate();
            else if (level.Neighbours.Contains(_level))
                level.Preload();
            else
                level.Deactivate();
        }

        CurrentLevelActive = _level;
    }

    public void UpdateLevelList()
    {
        Levels.Clear();
        foreach (Level level in FindObjectsOfType<Level>())
            Levels.Add(level);
    }
}