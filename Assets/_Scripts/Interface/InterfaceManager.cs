using popuphints;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private Canvas GameOverScreenCanvas;
    [Space]
    [Header("Visual Helpers")]
    [SerializeField] private GameObject playerCollisionVisualHelper;
    [Space]
    [SerializeField] private PlayerMovement playerMovement;



    private void Start()
    {
        inputManager.inputMaster.Interface.Pause.started += ShowPauseMenu;
        GameManager.instance.onPlayerDeath += DisplayGameOverScreen;
    }

    public void TogglePlayerCollisionVisualHelper()
    {
        if (GameManager.instance.gameState == GameState.Guide)
        {
            playerCollisionVisualHelper.SetActive(!playerCollisionVisualHelper.activeSelf);
        }
    }
    
    public void TogglePlayerVerticalMovement()
    {
        playerMovement.AllowMoveBack = !playerMovement.AllowMoveBack;
        playerMovement.AllowMoveForward = !playerMovement.AllowMoveForward;
    }

    public void TogglePlayerHorizontalMovement()
    {
        playerMovement.AllowMoveLeft = !playerMovement.AllowMoveLeft;
        playerMovement.AllowMoveRight = !playerMovement.AllowMoveRight;
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
        LevelManager.instance.RestartCurrentScene();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        LevelManager.instance.LoadScene(LevelManager.MainMenu);
    }

    private void ShowPauseMenu(InputAction.CallbackContext _)
    {
        bool isPauseMenuActivated = !pauseMenuCanvas.gameObject.activeSelf;

        if (isPauseMenuActivated)
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

    private void DisplayGameOverScreen()
    {
        if(GameOverScreenCanvas == null)
        {
            return;
        }

        Time.timeScale = 0f;
        GameManager.instance.EnableCursor();

        GameOverScreenCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        GameManager.instance.ExitGame();
    }
}
