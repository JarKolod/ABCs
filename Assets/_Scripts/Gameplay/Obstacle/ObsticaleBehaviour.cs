using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticaleBehaviour : MonoBehaviour
{
    [SerializeField] GameObject dustParticles;

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
                Destroy(gameObject);
                // end the run i guess
                break;
            }
        }
    }

    private void PlayParticlesEffect()
    {
        Vector3 spawnPoint = transform.position;
        spawnPoint.y = 0;
        spawnPoint.z -= 1.5f;

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
