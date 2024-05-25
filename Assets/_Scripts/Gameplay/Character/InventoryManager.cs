using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    InventoryStorage invStorage;

    public Action<int> coinCountChange;
    public Action<int> onScoreAmountChange;

    public int currentCoinAmount { get => invStorage.coinCount; }
    public int currentScoreAmount { get => invStorage.scoreAmount; }

    private void Awake()
    {
        invStorage = new InventoryStorage();
    }

    public void AddCoins(int amount)
    {
        invStorage.coinCount += amount;
        coinCountChange?.Invoke(amount);
    }

    public void AddToScore(int amount)
    {
        invStorage.scoreAmount += amount;
        onScoreAmountChange?.Invoke(amount);
    }
}
