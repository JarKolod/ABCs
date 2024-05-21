using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class CoinCollectionBehaviour : MonoBehaviour
{
    [SerializeField] InventoryManager playerInvManager;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] int ScoreAddedOnCoinPickup = 20;

    Animator coinTextAnimator;

    private void OnEnable()
    {
        coinTextAnimator = coinText.GetComponent<Animator>();
        playerInvManager.coinCountChange += UpdateDisplayedCoinAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Coin"))
        {
            playerInvManager.AddCoins(1);
            playerInvManager.AddToScore(ScoreAddedOnCoinPickup);
        }

        Destroy(other.gameObject);
    }

    private void UpdateDisplayedCoinAmount(int _)
    {
        coinText.SetText(playerInvManager.currentCoinAmount.ToString());
        coinTextAnimator.SetTrigger("Pop");
    }
}
