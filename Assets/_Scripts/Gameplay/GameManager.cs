using Newtonsoft.Json;
using popuphints;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private InventoryManager playerInvManager;

    private IDataService dataService = new JsonDataService();

    private void Awake()
    {
        CheckSingleton();

        // why did i need this?
        // trackManager = FindFirstObjectByType<TrackManager>();
    }


    private void Start()
    {
        //DisableCursor();
        playerInvManager = FindFirstObjectByType<InventoryManager>();

        InventoryStorage loadedInv = LoadHighScoreData();
        if (loadedInv.highScores.Count > 0)
        {
            playerInvManager.SwapHighScores(loadedInv);
        }
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

    public void SavePlayersHighScores()
    {
        // maybe create list with non level scenes? - too tiered to think about it now
        if (SceneManager.GetActiveScene().name != "Main_Menu")
        {
            playerInvManager.AddCurrentHighScoreDataToInv();
        }

        if (dataService.SaveData("/player_data.json", playerInvManager.InvStorage, false))
        {
            InventoryStorage invHighScoreDataAfterLoad = LoadHighScoreData();
            playerInvManager.SwapHighScores(invHighScoreDataAfterLoad);
        }
        else
        {
            Debug.LogError("Could not save file!");
        }
    }

    private InventoryStorage LoadHighScoreData()
    {
        try
        {
            InventoryStorage data = dataService.LoadData<InventoryStorage>("/player_data.json", false);
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not read file! err: " + e.Message + ", " + e.Source);
            throw new Exception("Could not read file! err: \" + e.Message + \", \" + e.Source"); // for handling later
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
