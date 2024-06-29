using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChartOnClickSetup : MonoBehaviour
{
    [SerializeField] Level Level;
    [SerializeField] UIChartController chartController;

    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChartSetup);
    }

    private void ChartSetup()
    {
        chartController.SetLevel(Level);
        chartController.FillChart();
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ChartSetup);
    }
}
