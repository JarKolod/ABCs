using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryStorage
{
    [NonSerialized]
    int _coinCount = 0;
    [NonSerialized]
    int _scoreCount = 0;
    Dictionary<string, Dictionary<DateTime,int>> _highScores = new(); // <level_name<date,score>>

    [JsonIgnore]
    public int coinCount { get => _coinCount; set { _coinCount = value > 0 ? value : 0; } }
    [JsonIgnore]
    public int scoreAmount { get => _scoreCount; set => _scoreCount = value > 0 ? value : 0; }
    public Dictionary<string, Dictionary<DateTime, int>> highScores { get => _highScores; set => _highScores = value; }
}
