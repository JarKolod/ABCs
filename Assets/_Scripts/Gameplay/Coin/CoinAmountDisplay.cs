using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinAmountDisplay : MonoBehaviour
{
    Animator coinNumberPopAnimator;

    [SerializeField] InventoryManager PlayerInvManager;
    [SerializeField] TextMeshProUGUI coinText;

    private void Start()
    {
        PlayerInvManager.coinCountChange += UpdateDisplayedCoinAmount;
        coinNumberPopAnimator = GetComponent<Animator>();
    }

    private void UpdateDisplayedCoinAmount(int amount)
    {
        coinText.SetText(amount.ToString());
        coinNumberPopAnimator.SetTrigger("Pop");
    }
}
