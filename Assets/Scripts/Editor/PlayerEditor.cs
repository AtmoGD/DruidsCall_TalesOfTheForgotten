using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    private Player player;

    private void OnEnable()
    {
        player = (Player)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Load"))
            player.LoadData();

        if (GUILayout.Button("Save"))
            player.SaveData();

        if (GUILayout.Button("Delete"))
            SaveSystem.DeleteData(player.Data.FileName);

        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
