using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    InventoryStorage invStorage;

    public Action<int> coinCountChange;
    public Action<int> scoreAmountChange;

    public int currentCoinAmount { get => invStorage.coinCount; }
    public int currentScoreAmount { get => invStorage.scoreAmount; }

    private void Start()
    {
        invStorage = new InventoryStorage();
    }

    public void AddCoins(int amount)
    {
        invStorage.coinCount += amount;
        coinCountChange?.Invoke(invStorage.coinCount);
        AddToScore(invStorage.coinCount);
    }

    public void AddToScore(int amount)
    {
        invStorage.scoreAmount += amount;
        scoreAmountChange?.Invoke(invStorage.scoreAmount);
    }
}
