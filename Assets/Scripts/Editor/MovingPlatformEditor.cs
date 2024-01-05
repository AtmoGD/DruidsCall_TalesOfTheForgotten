using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(MovingPlatform)), CanEditMultipleObjects]
public class MovingPlatformEditor : Editor
{
    private MovingPlatform movingPlatform;

    private void OnEnable()
    {
        movingPlatform = (MovingPlatform)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset Platform"))
        {
            foreach (Object target in targets)
            {
                MovingPlatform platform = (MovingPlatform)target;
                platform.ResetPlatform();
            }
        }
    }
}

#endif