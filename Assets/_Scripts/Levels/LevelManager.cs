using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public static string MainMenu { get => "Main_Menu"; }
    public static string GuideMovementLeftRight { get => "Guide_Movement_Left_Right"; }
    public static string ChallengeMovementLeftRight { get => "Challenge_Movement_Left_Right"; }

    private GameState currentGameState;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
