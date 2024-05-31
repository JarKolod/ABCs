using System;
using System.Data;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    InventoryStorage invStorage;

    public Action<int> coinCountChange;
    public Action<int> onScoreAmountChange;

    public readonly List<string> noHighScoreLevels = new(){ "Main_Menu", "Level_Template" };

    public int currentCoinAmount { get => invStorage.coinCount; }
    public int currentScoreAmount { get => invStorage.scoreAmount; }
    public InventoryStorage InvStorage { get => invStorage; }

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

    public void AddCurrentHighScoreDataToInv()
    {
        DateTime currentDate = DateTime.Now.Date;
        string sceneName = SceneManager.GetActiveScene().name;

        if (!invStorage.highScores.TryGetValue(sceneName, out var levelHighScores))
        {
            levelHighScores = new Dictionary<DateTime, int>();
            invStorage.highScores[sceneName] = levelHighScores;
        }

        if (!levelHighScores.TryGetValue(currentDate, out var storedHighScore) || storedHighScore < invStorage.scoreAmount)
        {
            levelHighScores[currentDate] = invStorage.scoreAmount;
        }
    }

    public void SwapHighScores(InventoryStorage inv)
    {
        if (inv == null)
        {
            Debug.LogError("Attempted to update with a null InventoryStorage instance.");
            return;
        }

        invStorage.highScores = new Dictionary<string, Dictionary<DateTime, int>>(inv.highScores);
    }

    public static Dictionary<string, Dictionary<DateTime, int>> MergeHighScores(
    Dictionary<string, Dictionary<DateTime, int>> firstHighScores,
    Dictionary<string, Dictionary<DateTime, int>> secondHighScores)
    {
        foreach (var levelEntry in secondHighScores)
        {
            string levelName = levelEntry.Key;
            Dictionary<DateTime, int> secondLevelScores = levelEntry.Value;

            if (!firstHighScores.ContainsKey(levelName))
            {
                firstHighScores[levelName] = new Dictionary<DateTime, int>(secondLevelScores);
            }
            else
            {
                Dictionary<DateTime, int> firstLevelScores = firstHighScores[levelName];

                foreach (var scoreEntry in secondLevelScores)
                {
                    DateTime scoreDate = scoreEntry.Key;
                    int secondScore = scoreEntry.Value;

                    if (!firstLevelScores.ContainsKey(scoreDate))
                    {
                        firstLevelScores[scoreDate] = secondScore;
                    }
                    else
                    {
                        int firstScore = firstLevelScores[scoreDate];
                        firstLevelScores[scoreDate] = Math.Max(firstScore, secondScore);
                    }
                }
            }
        }

        return firstHighScores;
    }

}
