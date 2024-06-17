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
    ChallengeMovementLeftRight
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    private static Level _selectedLevel = Level.None;
    public static Level selectedLevel { get => _selectedLevel; set => _selectedLevel = value; }

    // scene name should be changed here and in assets only
    public static string None { get => ""; }
    public static string MainMenu { get => "Main_Menu"; }
    public static string GuideMovementLeftRight { get => "Guide_Movement_Left_Right"; }
    public static string ChallengeMovementLeftRight { get => "Challenge_Movement_Left_Right"; }

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
            case Level.ChallengeMovementLeftRight:
                return ChallengeMovementLeftRight;
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
