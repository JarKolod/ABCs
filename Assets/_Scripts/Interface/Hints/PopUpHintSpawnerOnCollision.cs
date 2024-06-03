using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopUpHintSpawnerOnCollision : MonoBehaviour
{
    [SerializeField] GameObject hintPrefab;
    [SerializeField] LayerMask popUpOnCollisionLayer;

    static List<string> tagsHit = new();

    private void OnTriggerEnter(Collider other)
    {
        if (!tagsHit.Contains(transform.tag) && CommonUtils.IsLayerInLayerMask(other.gameObject.layer, popUpOnCollisionLayer) && hintPrefab != null)
        {
            tagsHit.Add(transform.tag);
            GameManager.instance.DisplayHint(hintPrefab);
        }
    }
}
