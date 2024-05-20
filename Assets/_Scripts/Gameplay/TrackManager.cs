using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public static Action<float> OnTrackSpeedChange; // takes trackSpeed as argument

    [Header("Setup")]
    [SerializeField] ObsticaleSpawner obsticaleSpawner;
    [SerializeField] GameObject spawningPoint;
    [SerializeField] List<GameObject> trackSections;
    [Space]
    [Header("Spawning properties")]
    [SerializeField] float _trackSpeed = 0.1f;
    [Range(0f, 1f)]
    [SerializeField] float trackAcceleration = 0.1f;
    [SerializeField] float distanceBetweenTrackElementSpawns = 30f;
    [Space]
    [Header("Track properties")]
    [SerializeField] float gameplayTimeAccelerationInterval = 30f;

    List<GameObject> spawnedTrackSections = new();

    float distanceTravelledSinceLastElementSpawn = 0f;
    int gameplayTimeAccelerationCount = 1;

    Coroutine mesureDistanceBetweenSpawns;

    private void OnEnable()
    {

        obsticaleSpawner.onTrackSectionExitingSpawnPoint += OnTrackElementEnd;
        StartCoroutine(InfiniteSpawnStart());
        StartCoroutine(UpdateTrackAcceleration());

    }

    private void Start()
    {
        OnTrackSpeedChange(_trackSpeed);
    }

    private void Update()
    {
        UpdateTrackElementPositions();
    }

    private IEnumerator UpdateTrackAcceleration()
    {
        while (true)
        {
            yield return new WaitUntil(() =>
            {
                return GameManager.gameplayTime > gameplayTimeAccelerationCount * gameplayTimeAccelerationInterval;
            });
            gameplayTimeAccelerationCount++;
            _trackSpeed += trackAcceleration * GameManager.gameplayTime * Time.deltaTime;
            OnTrackSpeedChange?.Invoke(_trackSpeed);
        }
    }

    private void OnTrackElementEnd()
    {
        StartCoroutine(UpdateDistanceBetweenSpawns());
    }

    private IEnumerator UpdateDistanceBetweenSpawns()
    {
        while (distanceTravelledSinceLastElementSpawn < distanceBetweenTrackElementSpawns)
        {
            yield return new WaitForSeconds(0.5f);
            distanceTravelledSinceLastElementSpawn += _trackSpeed; //cos z tym gdzie to dac?
        }
    }

    private void UpdateTrackElementPositions()
    {
        foreach (GameObject section in spawnedTrackSections)
        {
            section.transform.Translate(transform.forward * _trackSpeed * Time.deltaTime);
        }
    }

    private IEnumerator InfiniteSpawnStart()
    {
        while (true)
        {
            if (mesureDistanceBetweenSpawns != null)
            {
                StopCoroutine(mesureDistanceBetweenSpawns);
            }
            distanceTravelledSinceLastElementSpawn = 0;
            spawnedTrackSections.Add(obsticaleSpawner.Spawn(trackSections[UnityEngine.Random.Range(0, trackSections.Count)], spawningPoint.transform));
            yield return new WaitUntil(() => { return distanceTravelledSinceLastElementSpawn >= distanceBetweenTrackElementSpawns; });
        }
    }
}
