using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Game))]
public class GameEditor : Editor
{
    private Game game;

    private void OnEnable()
    {
        game = (Game)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Load Game 1"))
            game.LoadGame("Game1");

        if (GUILayout.Button("Load Game 2"))
            game.LoadGame("Game2");

        if (GUILayout.Button("Load Game 3"))
            game.LoadGame("Game3");

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Save Game"))
            game.SaveGame();

        if (GUILayout.Button("Find Save Points"))
            game.FindSavePoints();
    }
}
