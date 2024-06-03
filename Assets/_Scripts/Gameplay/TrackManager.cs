using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

// TODO: create a death plane for spawned track sections
public class TrackManager : MonoBehaviour
{
    public static Action<float> OnTrackSpeedChange; // takes trackSpeed as argument

    [Header("Setup")]
    [SerializeField] ObsticaleSpawner obsticaleSpawner;
    [SerializeField] float finalPositionOfSpawnPointOZ;
    [SerializeField] GameObject spawningPoint;
    [SerializeField] List<GameObject> trackSections;
    [Space]
    [Header("Spawning properties")]
    [SerializeField] float _trackSpeed = 0.1f;
    [Range(0f, 10f)]
    [SerializeField] float trackAccelerationScalar = 0.1f;
    [SerializeField] float _maxTrackSpeed = 10f;
    [SerializeField] float distanceBetweenTrackElementSpawns = 30f;
    [Space]
    [Header("Track properties")]
    [SerializeField] float trackAccelerationIntervalsSec = 30f;

    List<GameObject> spawnedTrackSections = new();

    float distanceTravelledSinceLastElementSpawn = 0f;
    int gameplayTimeAccelerationCount = 1;

    Coroutine measureDistanceBetweenSpawns;

    public float maxTrackSpeed { get => _maxTrackSpeed; }
    public float trackSpeed { get => _trackSpeed; }

    private void OnEnable()
    {
        obsticaleSpawner.onTrackSectionExitingSpawnPoint += OnTrackElementEnd;
        obsticaleSpawner.onTrackSectionEnteringSpawnPoint += UpdateSpawnerPosition;
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
        while (_maxTrackSpeed > _trackSpeed)
        {
            yield return new WaitUntil(() =>
            {
                return Time.timeSinceLevelLoad > gameplayTimeAccelerationCount * trackAccelerationIntervalsSec;
            });
            gameplayTimeAccelerationCount++;
            _trackSpeed += trackAccelerationScalar * Time.timeSinceLevelLoad * Time.deltaTime;
            OnTrackSpeedChange?.Invoke(_trackSpeed);
        }
        gameplayTimeAccelerationCount++;
        _trackSpeed = _maxTrackSpeed;
        OnTrackSpeedChange?.Invoke(_trackSpeed);
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
            distanceTravelledSinceLastElementSpawn += _trackSpeed; //cos z tym, gdzie to dac?
        }
    }

    private void UpdateTrackElementPositions()
    {
        foreach (GameObject section in spawnedTrackSections)
        {
            section.transform.Translate(transform.forward * _trackSpeed * Time.deltaTime);
        }
    }

    private void UpdateSpawnerPosition()
    {
        GameObject lastSection = spawnedTrackSections.Last();
        GameObject lastSectionEndPoint = CommonUtils.FindChildWithTag(lastSection, "TrackSectionEnd");
        if (lastSectionEndPoint != null)
        {
            if (lastSectionEndPoint.transform.position.z <= finalPositionOfSpawnPointOZ)
            {
                obsticaleSpawner.transform.position = new Vector3(obsticaleSpawner.transform.position.x, obsticaleSpawner.transform.position.y, finalPositionOfSpawnPointOZ);
                obsticaleSpawner.onTrackSectionEnteringSpawnPoint -= UpdateSpawnerPosition;
            }
            else
            {
                obsticaleSpawner.transform.position = obsticaleSpawner.transform.position = new Vector3(obsticaleSpawner.transform.position.x, obsticaleSpawner.transform.position.y, lastSectionEndPoint.transform.position.z);

            }
        }
        else
        {
            throw new Exception("Section does not have an end point!");
        }
    }

    private IEnumerator InfiniteSpawnStart()
    {
        while (true)
        {
            if (measureDistanceBetweenSpawns != null)
            {
                StopCoroutine(measureDistanceBetweenSpawns);
            }
            distanceTravelledSinceLastElementSpawn = 0;
            spawnedTrackSections.Add(obsticaleSpawner.Spawn(trackSections[UnityEngine.Random.Range(0, trackSections.Count)], spawningPoint.transform));
            yield return new WaitUntil(() => { return distanceTravelledSinceLastElementSpawn >= distanceBetweenTrackElementSpawns; });
        }
    }
}
