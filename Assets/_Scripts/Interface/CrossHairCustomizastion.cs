using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrossHairCustomizastion : MonoBehaviour
{
    List<RectTransform> crossHair = new List<RectTransform>();

    [SerializeField] float crossHairWidth = 4f;
    [SerializeField] float crossHairLength = 10f;

    private void OnEnable()
    {
        crossHair = GetComponentsInChildren<RectTransform>().Skip(1).ToList();
        UpdateCrossHair();
    }

    public void UpdateCrossHair()
    {
        foreach (var line in crossHair)
        {
            line.localScale = new Vector3(crossHairLength,crossHairWidth , line.localScale.z);
        }
    }

    private void OnValidate()
    {
        UpdateCrossHair();
    }
}
