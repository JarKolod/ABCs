using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingTargetBehaviour : MonoBehaviour, IDestructible
{
    [SerializeField] GameObject particlesOnDestroy;
    [SerializeField] Vector3 dustSpawnPointOffset;
    [SerializeField] int pointsOnDestroy = 50;
    [SerializeField] List<GameObject> listToDestroyOnHit;

    public int PointsOnDestroy { get => pointsOnDestroy; set => pointsOnDestroy = value; }

    public void OnDestroyObj()
    {
        PlayParticlesEffect();
        GameManager.instance.PlayerInvManager.AddToScore(pointsOnDestroy);

        foreach (GameObject obj in listToDestroyOnHit)
        {
            IDestructible destructible = obj.GetComponent<IDestructible>();

            if(destructible != null)
            {
                destructible.OnDestroyObj();
            }

            Destroy(obj.gameObject);
        }

        Destroy(gameObject);
    }

    public void DestroyTarget()
    {
        Destroy(gameObject);
    }

    private void PlayParticlesEffect()
    {
        Vector3 spawnPoint = transform.position;
        spawnPoint.x += dustSpawnPointOffset.x;
        spawnPoint.y += dustSpawnPointOffset.y;
        spawnPoint.z += dustSpawnPointOffset.z;

        GameObject effect = Instantiate(particlesOnDestroy, spawnPoint, transform.rotation);
        ParticleSystem ps = effect.GetComponent<ParticleSystem>();

        if (ps != null)
        {
            ps.Play();
            Destroy(ps, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(effect.gameObject);
        }
    }
}
