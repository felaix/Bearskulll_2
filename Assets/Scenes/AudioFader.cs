using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFader : MonoBehaviour
{
    public float fadeDuration = 2.0f;  // Duration of the fade in seconds. You can adjust this in the inspector.

    private AudioSource audioSource;
    float startVolume;


    private void Start()
    {
        startVolume = audioSource.volume;
        startVolume -= 0.1f;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.0f;

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = startVolume * (1 - t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop(); // You can comment out this line if you don't want to stop the audio after fading out.
    }
}
