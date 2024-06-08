using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObsticaleSpawner : MonoBehaviour
{
    [SerializeField] GameObject trackSectionSpawnsParent;

    public Action onTrackSectionExitingSpawnPoint;
    public Action onTrackSectionEnteringSpawnPoint;

    public GameObject Spawn(GameObject elementToSpawn, Transform spawnLocation)
    {
        return Instantiate(elementToSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (onTrackSectionExitingSpawnPoint != null && coll.gameObject.transform.tag.Equals("TrackSectionEnd"))
        {
            onTrackSectionExitingSpawnPoint();
        }

        if (onTrackSectionEnteringSpawnPoint != null && coll.gameObject.transform.tag.Equals("TrackSectionStart"))
        {
            onTrackSectionEnteringSpawnPoint();
        }
    }
}
