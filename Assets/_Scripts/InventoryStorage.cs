using UnityEngine;

public class InventoryStorage : MonoBehaviour
{
    int coinCount;

    public int CoinCount { get => coinCount; set { coinCount = value > 0 ? value : 0; } }
}
