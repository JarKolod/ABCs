using popuphints;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Canvas pauseMenuCanvas;
    [Header("Visual Helpers")]
    [SerializeField] private GameObject playerCollisionVisualHelper;


    private void Start()
    {
        inputManager.inputMaster.Interface.Pause.started += OnEscapePress;
    }

    public void TogglePlayerCollisionVisualHelper()
    {
        playerCollisionVisualHelper.SetActive(!playerCollisionVisualHelper.activeSelf);
    }

    public void ResumeGame()
    {
        if (!PopUpHintManager.instance.isHintBeingDisplayed())
        {
            Time.timeScale = 1f;
            GameManager.instance.DisableCursor();
        }
        pauseMenuCanvas.gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu(string mainMenuSceneName)
    {
        LevelManager.instance.LoadScene(mainMenuSceneName);
    }

    private void OnEscapePress(InputAction.CallbackContext _)
    {
        bool isPauseMenuActivated = !pauseMenuCanvas.gameObject.activeSelf;

        if(isPauseMenuActivated)
        {
            Time.timeScale = 0f;
            GameManager.instance.EnableCursor();
        }
        else
        {
            if (!PopUpHintManager.instance.isHintBeingDisplayed())
            { 
                Time.timeScale = 1f;
                GameManager.instance.DisableCursor();
            }
        }
        
        pauseMenuCanvas.gameObject.SetActive(isPauseMenuActivated);
    }
}
