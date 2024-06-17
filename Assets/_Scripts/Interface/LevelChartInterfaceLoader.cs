using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LevelChartInterfaceLoader : MonoBehaviour
{
    [Description("The canvas that contains the chart and")]
    [SerializeField] private Canvas chartCanvas;
    [SerializeField] private Level levelToGraph = Level.None;

    private UIChartController chartController;
    private LevelLoaderOnButtonEvent levelLoader;

    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
        chartController = chartCanvas.GetComponentInChildren<UIChartController>();
        levelLoader = chartCanvas.GetComponentInChildren<LevelLoaderOnButtonEvent>();

        if (chartController == null ) {
            Debug.LogError("Chart controller not found in children of canvas");
        }

        if ( levelLoader == null ) {
            Debug.LogError("Level loader not found in children of canvas");
        }
    }

    private void OnButtonClick()
    {
        chartController.SetLevel(levelToGraph);
        levelLoader.LevelToLoad = levelToGraph;
        chartCanvas.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(OnButtonClick);
    }
}
