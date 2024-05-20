using UnityEngine;

public class InventoryStorage
{
    int _coinCount;
    int _scoreCount;

    public int coinCount { get => _coinCount; set { _coinCount = value > 0 ? value : 0; } }
    public int scoreAmount { get => _scoreCount; set => _scoreCount = value > 0 ? value : 0; }
}
