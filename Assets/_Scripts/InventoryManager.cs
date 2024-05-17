using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventoryStorage invStorage;

    public Action<int> coinCountChange;

    public void AddCoins(int amount)
    {
        invStorage.CoinCount += amount;
        coinCountChange(invStorage.CoinCount);
    }
}
