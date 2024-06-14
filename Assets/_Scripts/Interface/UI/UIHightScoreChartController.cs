using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
public class UIHightScoreChartController : MonoBehaviour
{
    [SerializeField] Level levelSelected = Level.ChallengeMovementLeftRight;
    [Space]
    [SerializeField] private InventoryManager playerInvManager;
    [SerializeField] private RectTransform chartContainer;
    [SerializeField] private UILineRenderer uiLineRenderer;

    [SerializeField] private TMPro.TextMeshProUGUI firstXLabel;
    [SerializeField] private TMPro.TextMeshProUGUI lastXLabel;
    [SerializeField] private TMPro.TextMeshProUGUI firstYLabel;
    [SerializeField] private TMPro.TextMeshProUGUI lastYLabel;

    [SerializeField] private float bottomChartMargin = 20f;
    [SerializeField] private float topChartMargin = 20f;
    [SerializeField] private float leftChartMargin = 20f;
    [SerializeField] private float rightChartMargin = 20f;

    private InventoryStorage invStorage;

    private Dictionary<string, Dictionary<DateTime, int>> highScores;

    private void Start()
    {
        invStorage = new InventoryStorage(playerInvManager.InvStorage);

        // TODO: remove this line after testing
        StartCoroutine(FillChart());
    }

    // TODO: remove this method after testing
    IEnumerator FillChart()
    {
        yield return new WaitForSeconds(0.5f);
        FillChartWithHighScores(levelSelected);
        UpdateChart();
    }

    public void SetPoints(Vector2[] points)
    {
        uiLineRenderer.Points = points;
    }

    public void UpdateChart()
    {
        uiLineRenderer.SetAllDirty();
    }

    public void SetLevel(Level level)
    {
        levelSelected = level;
    }

    public Level GetLevel() 
    {
        return levelSelected;
    }

    public void DisplayChart()
    {
        FillChartWithHighScores(levelSelected);
        UpdateChart();
    }

    public void FillChartWithHighScores(Level level)
    {
        if (invStorage == null)
        {
            Debug.Log("Inventory storage is null");
            return;
        }

        Dictionary<DateTime, int> highScoresForScene = invStorage.highScores[LevelManager.instance.GetSceneName(level)];

        if (highScoresForScene == null || highScoresForScene.Count == 0)
        {
            Debug.Log("No high scores found for level: " + LevelManager.instance.GetSceneName(level));
            return;
        }

        List<DateTime> dates = new List<DateTime>(highScoresForScene.Keys);
        dates.Sort();

        FillLabels(highScoresForScene);

        Vector2[] points = CalculateChartPoints(dates, highScoresForScene);

        SetPoints(points);
        UpdateChart();
    }

    private void FillLabels(Dictionary<DateTime, int>  scoresWithDates)
    {
        firstYLabel.text = scoresWithDates.Values.Min().ToString();
        lastYLabel.text = scoresWithDates.Values.Max().ToString();
        firstXLabel.text = scoresWithDates.Keys.Min().ToString(format: "dd/MM/yyyy");
        lastXLabel.text = scoresWithDates.Keys.Max().ToString(format: "dd/MM/yyyy");
    }

    private Vector2[] CalculateChartPoints(List<DateTime> dates, Dictionary<DateTime, int> highScoresForScene)
    {
        if (dates.Count == 1)
        {
            return GenerateStraightLinePoints(highScoresForScene[dates[0]]);
        }
        else
        {
            return GeneratePoints(dates, highScoresForScene);
        }
    }

    private Vector2[] GenerateStraightLinePoints(float singleScore)
    {
        Vector2[] points = new Vector2[2];

        float positionY = -chartContainer.rect.height / 2f;
        float leftPositionX = -chartContainer.rect.width / 2f;
        float rightPositionX = chartContainer.rect.width / 2f;

        points[0] = new Vector2(leftPositionX, positionY);
        points[1] = new Vector2(rightPositionX, positionY);

        return points;
    }

    private Vector2[] GeneratePoints(List<DateTime> dates, Dictionary<DateTime, int> highScoresForScene)
    {
        Vector2[] points = new Vector2[dates.Count];

        float maxHighScore = highScoresForScene.Values.Max();
        float minHighScore = highScoresForScene.Values.Min();

        for (int i = 0; i < dates.Count; i++)
        {
            float normalizedScore = (highScoresForScene[dates[i]] - minHighScore) / (maxHighScore - minHighScore);
            float positionY = normalizedScore * (chartContainer.rect.height - topChartMargin - bottomChartMargin) - chartContainer.rect.height / 2f + bottomChartMargin;
            float positionX = (i * (chartContainer.rect.width - leftChartMargin)) / (dates.Count - 1) - (chartContainer.rect.width - leftChartMargin) / 2f + leftChartMargin;

            // add margin just for the last point
            if(i == dates.Count - 1)
            {
                positionX -= rightChartMargin;
            }

            points[i] = new Vector2(positionX, positionY);
        }

        return points;
    }


}