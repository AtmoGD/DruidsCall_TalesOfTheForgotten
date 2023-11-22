using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(World))]
public class WorldEditor : Editor
{
    private World world;

    private void OnEnable()
    {
        world = (World)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Update Level List"))
            world.UpdateLevelList();

        if (GUILayout.Button("Activate Level"))
            world.ActivateLevel();

        if (GUILayout.Button("Set All Levels Active"))
            world.SetAllLevelsActive();
    }
}