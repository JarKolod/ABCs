using System;
using TMPro;
using UnityEngine;

public class TimePlayedDisplay : MonoBehaviour
{
    TextMeshProUGUI elaspedTimeText;

    void Awake()
    {
        elaspedTimeText = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        elaspedTimeText.SetText(TimeSpan.FromSeconds(GameManager.gameplayTime).ToString(@"mm\:ss"));
    }
}
