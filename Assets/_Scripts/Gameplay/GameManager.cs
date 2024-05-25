using popuphints;
using System;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        CheckSingleton();

        // why did i need this?
        // trackManager = FindFirstObjectByType<TrackManager>();
    }

    private void Update()
    {
        print(Time.timeSinceLevelLoad);
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

    private void Start()
    {
        DisableCursor();
    }

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
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
