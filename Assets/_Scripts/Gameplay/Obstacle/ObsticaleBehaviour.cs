using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticaleBehaviour : MonoBehaviour
{
    [SerializeField] GameObject dustParticles;
    [SerializeField] Vector3 dustSpawnPointOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.tag.Equals("Player"))
        {
            return;
        }

        switch (GameManager.instance.gameState)
        {
            case GameState.Menu:
            {
                Debug.LogError("This should never happen, wtf.");
                throw new System.Exception("player collision in menu???");
            }
            case GameState.Guide:
            {
                PlayParticlesEffect();
                Destroy(gameObject);

                break;
            }
            case GameState.Challenge:
            {
                PlayParticlesEffect();
                GameManager.instance.PlayerHitObstacle();
                Destroy(gameObject);
                break;
            }
        }
    }

    private void PlayParticlesEffect()
    {
        Vector3 spawnPoint = transform.position;
        spawnPoint.x += dustSpawnPointOffset.x;
        spawnPoint.y += dustSpawnPointOffset.y;
        spawnPoint.z += dustSpawnPointOffset.z;

        GameObject effect = Instantiate(dustParticles, spawnPoint, transform.rotation);
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
