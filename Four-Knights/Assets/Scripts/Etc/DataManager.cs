using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager
{
    public static T LoadGameData<T>(string fileName) where T : Data
    {
        string filePath = Application.persistentDataPath + "/" + fileName + ".json";
        T data = (T)new Data();
        if(File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<T>(fromJsonData);
        }
        return data;
    }

    public static void SaveGameData<T>(string fileName,T data)where T : Data
    {
        string filePath = Application.persistentDataPath + "/" + fileName + ".json";
        string toJsonData = JsonUtility.ToJson(data,true);

        File.WriteAllText(filePath, toJsonData);
    }
}