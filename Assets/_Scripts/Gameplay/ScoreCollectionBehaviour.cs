using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class ScoreCollectionBehaviour : MonoBehaviour
{
    [SerializeField] InventoryManager playerInvManager;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [Description("uses speed of the track(rounded up) and multiplies with this amount")]
    [Range(0f,10f)]
    [SerializeField] int pointsGainPerDistanceScalar;
    [Range(0f,1f)]
    [SerializeField] float scoreUpdateTimeInterval = 0.5f;

    float currentTrackSpeed = 0;

    Coroutine scoreAddCoroutine = null;

    private void OnEnable()
    {
        TrackManager.OnTrackSpeedChange += UpdateStoredTrackSpeed;
        playerInvManager.onScoreAmountChange += UpdateScoreInstant;
        scoreAddCoroutine = StartCoroutine(AddScoreOnDistanceTravaled(scoreUpdateTimeInterval));
    }

    private IEnumerator AddScoreOnDistanceTravaled(float timeInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInterval);

            int calculatedScore = (int)Math.Ceiling(currentTrackSpeed * pointsGainPerDistanceScalar);
            playerInvManager.AddToScore(calculatedScore);

            scoreText.SetText(playerInvManager.currentScoreAmount.ToString());
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
