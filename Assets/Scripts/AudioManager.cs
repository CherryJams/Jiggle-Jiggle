using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource pianoMusic;
    [SerializeField] private AudioSource jiggleJiggle;

    public void SwitchToEndingMusic()
    {
        StartCoroutine(StartFade(pianoMusic, 0.5f, 0f));
        jiggleJiggle.Play();
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        yield break;
    }
}