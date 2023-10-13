using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static readonly string persistantPath = Application.persistentDataPath + "\\";
    public static void SaveData<T>(T _data, string _path)
    {
        try
        {
            Serialize(_data, _path);
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
        T result = default;
        return result;
    }

    public static bool DeleteData(string _path)
    {
        if (File.Exists(persistantPath + _path))
        {
            File.Delete(persistantPath + _path);
            return true;
        }

        return false;
    }

    private static void Serialize<T>(T _data, string _path)
    {
        CheckPath(_path);

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

            T data = (T)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
            Debug.LogError("FILE DOESN'T EXISTS ON GIVEN PATH!");

        return default;
    }

    private static void CheckPath(string _path)
    {
        string[] folders = _path.Split('\\');
        Array.Resize(ref folders, folders.Length - 1);

        string path = persistantPath;
        foreach (string folder in folders)
        {
            path += folder + "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
