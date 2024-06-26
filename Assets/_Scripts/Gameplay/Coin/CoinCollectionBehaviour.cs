using TMPro;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class CoinCollectionBehaviour : MonoBehaviour
{
    [SerializeField] InventoryManager playerInvManager;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] int ScoreAddedOnCoinPickup = 20;

    Animator coinTextAnimator;

    private void Awake()
    {
        coinTextAnimator = coinText.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerInvManager.coinCountChange += UpdateDisplayedCoinAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Coin"))
        {
            playerInvManager.AddCoins(1);
			playerInvManager.AddToScore(ScoreAddedOnCoinPickup);

            AudioSource coinSound = other.gameObject.GetComponent<AudioSource>();

            if(coinSound)
            {
                coinSound.Play();
                Destroy(other.gameObject, coinSound.clip.length);
            }
            else
            {
                Destroy(other.gameObject);
            }

        }
    }

    private void UpdateDisplayedCoinAmount(int _)
    {
        coinText.SetText(playerInvManager.currentCoinAmount.ToString());
        coinTextAnimator.SetTrigger("Pop");
    }
}
