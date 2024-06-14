using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[System.Serializable]
public class InventoryStorage
{
    [NonSerialized]
    int _currCoinCount = 0;
    [NonSerialized]
    int _scoreCount = 0;

    int _totalCoinCount;
    Dictionary<string, Dictionary<DateTime,int>> _highScores = new(); // <level_name<date,score>>

    [JsonIgnore]
    public int currCoinCount { get => _currCoinCount; set { _currCoinCount = value > 0 ? value : 0; } }
    [JsonIgnore]
    public int scoreAmount { get => _scoreCount; set => _scoreCount = value > 0 ? value : 0; }
    public Dictionary<string, Dictionary<DateTime, int>> highScores { get => _highScores; set => _highScores = value; }
    public int totalCoinCount { get => _totalCoinCount; set => _totalCoinCount = value; }

    public InventoryStorage() { }
    public InventoryStorage(InventoryStorage inv) 
    {
        this.highScores = new Dictionary<string, Dictionary<DateTime, int>>();
        foreach (var outerPair in inv.highScores)
        {
            var innerDict = new Dictionary<DateTime, int>(outerPair.Value);
            this.highScores.Add(outerPair.Key, innerDict);
        }

        this.totalCoinCount = inv.totalCoinCount;
        this.scoreAmount = inv.scoreAmount;
        this.currCoinCount = inv.currCoinCount;
    }
}
