using Newtonsoft.Json;
using popuphints;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Loading;
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

    public GameState gameState { get => _gameState; set => _gameState = value; }
    public Action onPlayerDeath;

    private GameState _gameState = GameState.Guide;
    private InventoryManager playerInvManager;
    private IDataService dataService = new JsonDataService();
    private bool isHighScoreSavingAllowed = true;


    private void Awake()
    {
        CheckSingleton();
        SceneManager.sceneLoaded += OnSceneLoadSetup;
        SceneManager.sceneLoaded += LoadInventoryOnSceneLoad;
    }

    private void Start()
    {
        OnSceneLoadSetup(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }

    public void LoadInventoryOnSceneLoad(Scene scene, LoadSceneMode loadMode)
    {
        InventoryStorage loadedStorage = LoadPlayerInventory();
        playerInvManager.InvStorage.totalCoinCount = loadedStorage.totalCoinCount;
        playerInvManager.InvStorage.highScores = loadedStorage.highScores;
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
        _gameState = gameState;
    }

    private void OnSceneLoadSetup(Scene scene, LoadSceneMode loadMode)
    {
        playerInvManager = FindFirstObjectByType<InventoryManager>();

        isHighScoreSavingAllowed = playerInvManager != null && _gameState == GameState.Challenge ? true : false;
    }

    public bool SavePlayerInventory()
    {
        if (!isHighScoreSavingAllowed)
        {
            Debug.Log("Saving is not allowed in this scene: " + SceneManager.GetActiveScene().name);
            return false;
        }

        if (dataService.SaveData("/player_data.json", playerInvManager.InvStorage, false))
        {
            InventoryStorage invPlayerAfterLoad = LoadPlayerInventory();
            playerInvManager.SwapHighScores(invPlayerAfterLoad);
            playerInvManager.SwapTotalCoins(invPlayerAfterLoad);
            return true;
        }
        else
        {
            Debug.LogError("Could not save file!");
            return true;
        }
    }

    private InventoryStorage LoadPlayerInventory()
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
        if (gameState == GameState.Guide)
        {
            Time.timeScale = 0.0f;
            EnableCursor();
            PopUpHintManager.instance.InstantiatePopUpHint(hintPrefab);
        }
    }

    public void OnHintDestroy()
    {
        DisableCursor();
        Time.timeScale = 1.0f;
    }

    public void PlayerHitObstacle()
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath();
        }

        SavePlayerInventory();
    }
}
