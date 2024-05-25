using UnityEngine;

public class PopUpHintSpawnerOnPlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject hintPrefab;
    [SerializeField] LayerMask popUpOnCollisionLayer; // asigned "Player" in editor

    bool hasHintBeenDisplayer = false;  

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHintBeenDisplayer && CommonUtils.IsLayerInLayerMask(other.gameObject.layer, popUpOnCollisionLayer) && hintPrefab != null)
        {
            hasHintBeenDisplayer = true;
            GameManager.instance.DisplayHint(hintPrefab);
        }
    }
}
