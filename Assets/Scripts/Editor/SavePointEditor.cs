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
        savePoint.Init();

        initialized = true;
    }
}
