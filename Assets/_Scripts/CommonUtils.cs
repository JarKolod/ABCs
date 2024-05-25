using Unity.VisualScripting;
using UnityEngine;

public class CommonUtils
{
    private static CommonUtils _instance;
    private CommonUtils() { }

    public static CommonUtils Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CommonUtils();
            }
            return _instance;
        }
    }

    public static bool IsLayerInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask & (1 << layer)) != 0;
    }

}
