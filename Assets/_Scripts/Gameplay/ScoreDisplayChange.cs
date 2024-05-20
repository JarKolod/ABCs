using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ScoreDisplayChange : MonoBehaviour
{
    [SerializeField] InventoryManager PlayerInvManager;
    [SerializeField] TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        PlayerInvManager.scoreAmountChange += UpdateDisplayedScoreAmount;
    }

    private void UpdateDisplayedScoreAmount(int amount)
    {
        scoreText.SetText(amount.ToString());
    }
}
