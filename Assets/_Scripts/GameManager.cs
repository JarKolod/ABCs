using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static float _gameplayTime = 0f;

    public static Action onGameplayPause;
    public static Action onGameplayContinue;

    public static Action onEveryFrameOfGameplay;

    public static float gameplayTime { get => _gameplayTime; }

    private void Start()
    {
        onGameplayPause += EnableCursor;
        onGameplayPause += DisableGameplayTimeMessurment;

        onGameplayContinue += DisableCursor;
        onGameplayContinue += EnableGameplayTimeMessurment;

        onEveryFrameOfGameplay += MeasureGameplayTime;
    }

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
    }

    private void LateUpdate()
    {
        onEveryFrameOfGameplay();
    }

    private void MeasureGameplayTime()
    {
        _gameplayTime += Time.deltaTime;
    }

    private void EnableGameplayTimeMessurment()
    {
        if (!onEveryFrameOfGameplay.GetInvocationList().Contains((Action)MeasureGameplayTime))
        {
            onEveryFrameOfGameplay += MeasureGameplayTime;
        }
    }

    private void DisableGameplayTimeMessurment()
    {
        onEveryFrameOfGameplay -= MeasureGameplayTime;
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
