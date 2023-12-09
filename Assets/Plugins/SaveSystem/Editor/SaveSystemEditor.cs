using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditorInternal.VersionControl;

public class SaveSystemEditor : EditorWindow
{
    [MenuItem("Tools/Saved Games")]
    public static void ShowWindow()
    {
        GetWindow<SaveSystemEditor>("Saved Games");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        Button refreshButton = new();
        refreshButton.text = "Refresh";
        refreshButton.clicked += () => RefreshSavesList(root);
        root.Add(refreshButton);

        List<string> savesList = SaveSystem.LoadSavesList();

        for (int i = 0; i < savesList.Count; i++)
        {
            VisualElement container = new();
            container.style.flexDirection = FlexDirection.Row;

            Box backgroundBox = new();

            if (i % 2 == 0)
                backgroundBox.style.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
            else
                backgroundBox.style.backgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f, 0.5f);

            backgroundBox.style.flexGrow = 1f;
            backgroundBox.style.flexDirection = FlexDirection.Row;
            container.Add(backgroundBox);

            Label indexLabel = new();
            indexLabel.style.width = 20f;
            indexLabel.style.unityTextAlign = TextAnchor.MiddleLeft;
            indexLabel.text = (i + 1).ToString();
            backgroundBox.Add(indexLabel);

            Label saveNameLabel = new();
            saveNameLabel.style.flexGrow = 1f;
            saveNameLabel.style.unityTextAlign = TextAnchor.MiddleLeft;
            saveNameLabel.text = savesList[i];
            backgroundBox.Add(saveNameLabel);

            Label saveDataLabel = new(); // Just a hack store the save name
            saveDataLabel.text = savesList[i];

            Button deleteButton = new() { text = "Delete" };
            deleteButton.style.unityTextAlign = TextAnchor.MiddleRight;
            deleteButton.clicked += () => DeleteSave(saveDataLabel.text);
            backgroundBox.Add(deleteButton);

            root.Add(container);
        }

    }

    private void DeleteSave(string _saveName)
    {
        SaveSystem.DeleteData(_saveName);
        RefreshSavesList(rootVisualElement);
    }

    private void RefreshSavesList(VisualElement _root)
    {
        _root.Clear();

        CreateGUI();
    }
}


