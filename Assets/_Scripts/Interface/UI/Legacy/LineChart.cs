using UnityEngine;
using UnityEngine.UIElements;

public class LineChart : VisualElement
{
    private Vector2[] dataPoints;

    public LineChart(Vector2[] points)
    {
        dataPoints = points;
        generateVisualContent += OnGenerateVisualContent;
    }

    void OnGenerateVisualContent(MeshGenerationContext mgc)
    {
        var painter = mgc.painter2D;
        painter.lineWidth = 2;

        for (int i = 0; i < dataPoints.Length - 1; i++)
        {
            painter.BeginPath();
            painter.MoveTo(dataPoints[i]);
            painter.LineTo(dataPoints[i + 1]);
            painter.Stroke();
        }
    }
}
