using Unity.VisualScripting;
using UnityEngine;

public static class CommonUtils
{
    public static bool IsLayerInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask & (1 << layer)) != 0;
    }
}
