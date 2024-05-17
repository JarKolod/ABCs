using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class CoinCollectionBehaviour : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Coin"))
        {
            inventoryManager.AddCoins(1);
        }

        Destroy(other.gameObject);
    }
}
