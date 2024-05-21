using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ScoreCollectionBehaviour : MonoBehaviour
{
    [SerializeField] InventoryManager playerInvManager;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [Description("uses speed of the track(rounded up) and multiplies with this amount")]
    [Range(0f,10f)]
    [SerializeField] int amountGainPerUnitDistance;
    [Range(0f,1f)]
    [SerializeField] float scoreUpdateTimeInterval = 0.5f;

    float currentTrackSpeed = 0;

    Coroutine scoreAddCoroutine = null;

    private void OnEnable()
    {
        TrackManager.OnTrackSpeedChange += UpdateStoredTrackSpeed;
    }

    private void Start()
    {
        scoreAddCoroutine = StartCoroutine(AddScoreOnDistanceTravaled(scoreUpdateTimeInterval));
        playerInvManager.onScoreAmountChange += UpdateScoreInstant;
    }

    private IEnumerator AddScoreOnDistanceTravaled(float timeInterval)
    {
        while (true)
        {
            int calculatedScore = (int)Math.Ceiling(currentTrackSpeed * amountGainPerUnitDistance);
            playerInvManager.AddToScore(calculatedScore);

            scoreText.SetText(playerInvManager.currentScoreAmount.ToString());

            yield return new WaitForSeconds(timeInterval);
        }
    }

    public void UpdateScoreInstant(int _)
    {
        scoreText.SetText(playerInvManager.currentScoreAmount.ToString());
    }

    private void UpdateStoredTrackSpeed(float newTrackSpeed)
    {
        currentTrackSpeed = newTrackSpeed;
    }
}
