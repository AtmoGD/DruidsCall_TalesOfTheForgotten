using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SavePoint))]
public class SavePointEditor : Editor
{
    private SavePoint savePoint;
    private bool initialized = false;

    private void OnEnable()
    {
        savePoint = (SavePoint)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!initialized)
            Init();

        if (GUILayout.Button("Init"))
            Init();
    }

    private void Init()
    {
        World world = savePoint.GetComponentInParent<World>();
        if (world)
            savePoint.Data.World = world.WorldType;
        else
        {
            Debug.LogError("SavePointEditor: No World found in parent!");
            return;
        }

        savePoint.Data.SavePointName = System.Guid.NewGuid().ToString();

        initialized = true;
    }
}
