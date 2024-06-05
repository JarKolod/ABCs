using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas guideSelectionCanvas;
    [SerializeField] Canvas chalangeSelectionCanvas;

    Canvas currCanvas;
    Canvas prevCanvas;

    void Start()
    {
        GameManager.instance.EnableCursor();
        GameManager.instance.gameState = GameState.Menu;

        swapCurrentCanvas(mainMenuCanvas);
    }

    public void DisplayMainMenu()
    {
        swapCurrentCanvas(mainMenuCanvas);
    }

    public void DisplayGuideLevels()
    {
        swapCurrentCanvas(guideSelectionCanvas);
    }

    public void DisplayChallengeLevels() 
    {
        swapCurrentCanvas(chalangeSelectionCanvas);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void swapCurrentCanvas(Canvas canvas)
    {
        if(canvas == null)
        {
            Debug.LogError("canvas passed to the MainMenuManager.swapCurrentCanvas() is null");
            return;
        }

        if (currCanvas != null)
        {
            currCanvas.gameObject.SetActive(false);
        }

        prevCanvas = currCanvas;
        currCanvas = canvas;
        currCanvas.gameObject.SetActive(true);
    }

}
