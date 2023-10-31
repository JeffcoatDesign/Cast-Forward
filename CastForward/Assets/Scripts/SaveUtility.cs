using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveUtility
{
    //public static void SaveWorld(SaveData data, string saveSlot)
    //{
    //    string json = JsonUtility.ToJson(data);
    //    string path = Path.Combine(Application.persistentDataPath, (saveSlot + "World.json"));
    //    File.WriteAllText(path, json);
    //}

    //public static SaveData LoadWorld(string saveSlot)
    //{
    //    string path = Path.Combine(Application.persistentDataPath, (saveSlot + "World.json"));
    //    if (File.Exists(path))
    //    {
    //        string jsonString = File.ReadAllText(path);
    //        SaveData data = JsonUtility.FromJson<SaveData>(jsonString);
    //        return data;
    //    }
    //    else
    //    {
    //        Debug.LogError("Save file not found at: " + path);
    //        return null;
    //    }
    //}
}
