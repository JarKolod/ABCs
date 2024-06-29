using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementGuideManager : MonoBehaviour
{
    public static MovementGuideManager instance { get; private set; }

    private void Awake()
    {
        CheckSingleton();
    }

    private void Start()
    {
        GameManager.instance.SetGameState(GameState.Guide);
        GameManager.instance.DisableCursor();
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
