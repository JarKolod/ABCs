using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class JsonDataService : IDataService
{
    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        try
        {
            if (File.Exists(path))
            {
                Debug.Log("Data exists. Deleting old file and writing a new one!");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Writing file for the first time!");
            }

            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }


    public bool LoadData<T>(string relativePath, out T data, bool encrypted) where T : class, new()
    {
        string path = Application.persistentDataPath + relativePath;

        if (!File.Exists(path))
        {
            Debug.LogWarning($"Cannot load file at {path}. File does not exist, returning null!");
            data = null;
            return false;
        }

        try
        {
            string fileContent = File.ReadAllText(path);
            data = JsonConvert.DeserializeObject<T>(fileContent);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading file at {path}. Exception: {ex.Message}");
            data = null;
            return false;
        }
    }
}