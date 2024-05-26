using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField] private GameData gameData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        string jsonData = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + "/gamestate.json", jsonData);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/gamestate.json";
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(jsonData);
        }
    }
}

[System.Serializable]
public class GameData : ScriptableObject
{
    public int score;
    public DateTime timestamp;
    // Add other game state variables as needed
}