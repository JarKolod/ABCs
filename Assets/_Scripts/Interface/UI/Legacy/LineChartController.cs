using System.Collections;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public class LineChartController : MonoBehaviour
{
    public UIDocument uiDocument; // Ensure this is assigned in the Inspector

    private Vector2[] dataPoints = new Vector2[]
    {
        new Vector2(10, 30),
        new Vector2(20, 40)
    };

    void Start()
    {
        StartCoroutine(foo());
    }

    IEnumerator foo()
    {
        yield return new WaitForSeconds(0.5f);
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument is not assigned.");
            yield break;
        }

        VisualElement root = uiDocument.rootVisualElement;
        VisualElement chartContainer = root.Q<VisualElement>("ChartContainer"); // Create a container for the line chart
        print(chartContainer.layout.center);
        dataPoints[0].x = chartContainer.layout.xMin;// chartContainer.layout.min.x; // chartContainer.layout.xMin - chartContainer.layout.width/2f;
        dataPoints[1].x = chartContainer.layout.max.x; // Adjust the x position of the first data point
        //float width = chartContainer.layout.width;
        //float xPosition = chartContainer.layout.x;

        LineChart lineChart = new LineChart(dataPoints);

        chartContainer.Add(lineChart); // Add the line chart to the root
    }
}