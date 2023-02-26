using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Events

    public static event Action<AudioClip> PlayedSoundFX;
    public static event Action<AudioClip> PlayedMainAudio;

    public static void PlaySoundFX(AudioClip clip) => PlayedSoundFX?.Invoke(clip);
    public static void PlayMainAudio(AudioClip clip) => PlayedMainAudio?.Invoke(clip);

    #endregion

    public AudioSource mainAudio;
    public AudioSource soundFX;

    public static float maxVolume;
    bool isSoundFXPlaying;

    private void OnEnable()
    {
        PlayedSoundFX += OnPlaySoundFX;
        PlayedMainAudio += OnPlayMainAudio;
    }

    private void OnDisable()
    {
        PlayedSoundFX -= OnPlaySoundFX;
        PlayedMainAudio -= OnPlayMainAudio;
    }

    private void Start()
    {
        SetVolume(0.3f);
    }

    private void Update()
    {
        if (!soundFX.isPlaying && isSoundFXPlaying)
            OnSoundFXStop();
    }

    public void SetVolume(float volume)
    {
        maxVolume = volume;
        if (mainAudio.volume > maxVolume)
            mainAudio.volume = maxVolume;
        if (soundFX.volume > maxVolume)
            soundFX.volume = maxVolume;
    }

    private void OnPlayMainAudio(AudioClip clip)
    {

    }

    private void OnPlaySoundFX(AudioClip clip)
    {
        isSoundFXPlaying = true;
        mainAudio.volume = 0f;
        soundFX.Stop();
        soundFX.clip = clip;
        soundFX.Play();
    }

    private void OnSoundFXStop()
    {
        isSoundFXPlaying = false;
        mainAudio.volume = maxVolume;
    }
}