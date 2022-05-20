using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public static class FonduSonore
{
    public static IEnumerator StartFade(AudioMixerGroup audioMixer, float duration, float targetVolume)
    {
        float currentTime = 0;
        audioMixer.audioMixer.GetFloat("Volume", out float startVolume);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioMixer.audioMixer.SetFloat("Volume", Mathf.Lerp(startVolume, targetVolume, currentTime / duration));
            yield return null;
        }
        yield break;
    }
}
