using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerMusicPlayer : MonoBehaviour
{
    [SerializeField] float startMusicVolume = 1f;
    [SerializeField] float startMusicPitch = 1f;
    [SerializeField] float maxMusicVolume = 1f;
    [SerializeField] float maxMusicPitch = 1f;
    [SerializeField] float musicVolumeChangeSpeed = 0.1f;
    [SerializeField] float musicSpeedChangePitch = 0.1f;
    [SerializeField] float secondsToUpdateMusic = 0.05f;

    AudioSource musicSource;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        musicSource.volume = startMusicVolume;
        musicSource.pitch = startMusicPitch;

        StartCoroutine(MusicPropertyUpdate());
    }

    IEnumerator MusicPropertyUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsToUpdateMusic);
            if (musicSource.volume < maxMusicVolume)
            {
                musicSource.volume += (maxMusicVolume - startMusicVolume) * musicVolumeChangeSpeed;
            }

            if (musicSource.pitch < maxMusicPitch)
            {
                musicSource.pitch += (maxMusicPitch - startMusicPitch) * musicSpeedChangePitch;
            }
        }
    }
}
