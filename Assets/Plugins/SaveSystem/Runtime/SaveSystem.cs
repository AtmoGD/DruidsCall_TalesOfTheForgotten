using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/*
KNOWN LIMITATIONS:
- Can't save to subfolders
*/

public static class SaveSystem
{
    private static readonly string persistantPath = Application.persistentDataPath + "\\";
    private static readonly string savesListName = "GameSaves.list";
    private static List<string> savesList = new();

    public static void SaveData<T>(T _data, string _path, bool _addToSavesList = true)
    {
        try
        {
            Serialize(_data, _path);

            if (_addToSavesList)
                AddToSavesList(_path);
        }
        catch (Exception e)
        {
            Debug.LogError("CAN'T SAVE DATA! - REASON: " + e.Message);
            DeleteData(_path);
        }
    }

    public static T LoadData<T>(string _path)
    {
        try
        {
            return Deserialize<T>(_path);
        }
        catch (Exception e)
        {
            Debug.LogError("CAN'T LOAD DATA! - REASON: " + e.Message);
        }

        return default;
    }

    public static bool DeleteData(string _path)
    {
        if (!File.Exists(persistantPath + _path)) return false;

        File.Delete(persistantPath + _path);

        RemoveFromSavesList(_path);

        return true;
    }

    private static void Serialize<T>(T _data, string _path)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(persistantPath + _path, FileMode.Create);

        formatter.Serialize(stream, _data);
        stream.Close();
    }

    private static T Deserialize<T>(string _path)
    {
        if (File.Exists(persistantPath + _path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(persistantPath + _path, FileMode.Open);

            T data = default;

            if (stream.Length != 0)
                data = (T)formatter.Deserialize(stream);

            stream.Close();

            return data;
        }
        else
            Debug.LogError("FILE DOESN'T EXISTS ON GIVEN PATH!");

        return default;
    }

    public static List<string> LoadSavesList()
    {
        savesList = Deserialize<List<string>>(savesListName);
        savesList ??= new List<string>();
        return savesList;
    }

    private static void SaveSavesList()
    {
        Serialize(savesList, savesListName);
        LoadSavesList();
    }

    public static void AddToSavesList(string _path)
    {
        LoadSavesList();

        if (!savesList.Contains(_path))
            savesList.Add(_path);

        SaveSavesList();
    }

    public static void RemoveFromSavesList(string _path)
    {
        LoadSavesList();

        if (savesList.Contains(_path))
            savesList.Remove(_path);

        SaveSavesList();
    }
}
