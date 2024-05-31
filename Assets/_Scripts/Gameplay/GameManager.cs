using Newtonsoft.Json;
using popuphints;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Guide,
    Challenge,
    Menu
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private GameState _gameState;
    private InventoryManager playerInvManager;

    private IDataService dataService = new JsonDataService();

    private bool isHighScoreSavingAllowed = true;

    [SerializeField] List<string> nonHighScoreScenes = new();

    public GameState gameState { get => _gameState; }

    private void Awake()
    {
        CheckSingleton();
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void Start()
    {
        DisableCursor();
        OnSceneLoad(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }

    private void CheckSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameState(GameState gameState)
    {
        this._gameState = gameState;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadMode)
    {
        playerInvManager = FindFirstObjectByType<InventoryManager>();

        isHighScoreSavingAllowed = playerInvManager == null ? false : true;
    }

    public bool SavePlayersHighScores()
    {
        if(!isHighScoreSavingAllowed)
        {
            Debug.Log("Saving is not allowed in this scene: " + SceneManager.GetActiveScene().name);
            return false;
        }

        if (!nonHighScoreScenes.Contains(SceneManager.GetActiveScene().name))
        {
            playerInvManager.AddCurrentHighScoreDataToInv();
        }

        if (dataService.SaveData("/player_data.json", playerInvManager.InvStorage, false))
        {
            InventoryStorage invHighScoreDataAfterLoad = LoadHighScoreData();
            playerInvManager.SwapHighScores(invHighScoreDataAfterLoad);
            return true;
        }
        else
        {
            Debug.LogError("Could not save file!");
            return true;
        }
    }

    private InventoryStorage LoadHighScoreData()
    {
        try
        {
            if (dataService.LoadData("/player_data.json", out InventoryStorage data, false))
            {
                return data;
            }
            else
            {
                return new InventoryStorage();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not read file! err: " + e.Message + ", " + e.Source);
            return new InventoryStorage();
        }
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisplayHint(GameObject hintPrefab)
    {
        Time.timeScale = 0.0f;
        EnableCursor();
        PopUpHintManager.instance.InstantiatePopUpHint(hintPrefab);
    }

    public void OnHintDestroy()
    {
        DisableCursor();
        Time.timeScale = 1.0f;
    }
}
