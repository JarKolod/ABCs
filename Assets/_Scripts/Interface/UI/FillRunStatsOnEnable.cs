using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillRunStatsOnEnable : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI stats;

    private void OnEnable()
    {
        int score = GameManager.instance.PlayerInvManager.currentScoreAmount;
        int coins = GameManager.instance.PlayerInvManager.InvStorage.currCoinCount;

        stats.text = $"Score: {score}\nCoins collected: {coins}";
    }
}
