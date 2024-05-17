using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAmountDisplay : MonoBehaviour
{
    [SerializeField] InventoryManager PlayerInvManager;
    [SerializeField] TMPro.TextMeshProUGUI coinText;


    private void Start()
    {
        PlayerInvManager.coinCountChange += SetDisplayedCoinAmount;
    }

    private void SetDisplayedCoinAmount(int amount)
    {
        coinText.SetText(amount.ToString());
    }
}
