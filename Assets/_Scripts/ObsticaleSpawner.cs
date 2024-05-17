using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObsticaleSpawner : MonoBehaviour
{
    [SerializeField] GameObject trackSectionSpawnsParent;

    public Action onTrackSectionExitingSpawnPoint;

    public GameObject Spawn(GameObject elementToSpawn, Transform spawnLocation)
    {
        return Instantiate(elementToSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation, trackSectionSpawnsParent.transform);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.transform.tag.Equals("TrackSectionEnd"))
        {
            onTrackSectionExitingSpawnPoint();
        }
    }
}
