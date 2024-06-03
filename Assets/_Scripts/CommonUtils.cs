using Unity.VisualScripting;
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
}
