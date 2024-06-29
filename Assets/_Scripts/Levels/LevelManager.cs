using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Level
{
    None,
    MainMenu,
    GuideMovementLeftRight,
    ChallengeMovementLeftRight,
    GuideMovementAim,
    ChallengeMovementAim
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    private static Level _selectedLevel = Level.None;
    public static Level selectedLevel { get => _selectedLevel; set => _selectedLevel = value; }

    // scene names should correspond to the names in the build settings and the scene asset names and be defined only here
    public static string None { get => ""; }
    public static string MainMenu { get => "Main_Menu"; }
    public static string GuideMovementLeftRight { get => "Guide_Movement_Left_Right"; }
    public static string ChallengeMovementLeftRight { get => "Challenge_Movement_Left_Right"; }
    public static string ChallengeMovementAim { get => "Challenge_Movement_Aim"; }
    public static string GuideMovementAim { get => "Guide_Movement_Aim"; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string GetSceneName(Level level)
    {
        switch (level)
        {
            case Level.None:
                return "";
            case Level.MainMenu:
                return MainMenu;
            case Level.GuideMovementLeftRight:
                return GuideMovementLeftRight;
            case Level.GuideMovementAim:
                return GuideMovementAim;
            case Level.ChallengeMovementLeftRight:
                return ChallengeMovementLeftRight;
            case Level.ChallengeMovementAim:
                return ChallengeMovementAim;
            default:
                return "";
        }
    }

    public void LoadSelectedLevel()
    {
        if (selectedLevel == Level.None)
        {
            return;
        }
        LoadLevel(selectedLevel);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevel(Level level)
    {
        string sceneName = GetSceneName(level);
        if(sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
