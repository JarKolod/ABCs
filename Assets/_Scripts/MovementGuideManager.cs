using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementGuideManager : MonoBehaviour
{
    public static MovementGuideManager instance { get; private set; }

    [SerializeField] GameObject onLevelStartHint;

    private void Awake()
    {
        CheckSingleton();
    }

    private void Start()
    {
        GameManager.instance.SetGameState(GameState.Guide);
        GameManager.instance.DisableCursor();
        Invoke("DisplayHintOnStart", 0.5f);
    }


    private void DisplayHintOnStart()
    {
        if (onLevelStartHint)
        {
            GameManager.instance.DisplayHint(onLevelStartHint);
        }
        else
        {
            Debug.Log("No Hint on Start up");
        }
    }

    private void CheckSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
