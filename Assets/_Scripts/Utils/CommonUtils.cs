using System.Collections.Generic;
using System;
using UnityEngine;

public static class CommonUtils
{
    public static bool IsLayerInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask & (1 << layer)) != 0;
    }
    public static GameObject FindChildWithTag(GameObject parent, string tag)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public static Vector2 GetRectTransformCenter(RectTransform rect)
    {
        if (rect != null)
        {
            Vector2 pivotOffset = new Vector2(
                rect.rect.width * (0.5f - rect.pivot.x),
                rect.rect.height * (0.5f - rect.pivot.y)
            );
            return rect.anchoredPosition + pivotOffset;
        }
        return Vector2.zero;
    }

    public static void PrintDictionary(Dictionary<DateTime, int> dictionary)
    {
        foreach (var entry in dictionary)
        {
            Debug.Log($"Key: {entry.Key}, Value: {entry.Value}");
        }
    }
}
