using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGLAudio : MonoBehaviour
{
    [SerializeField] AudioClip _clip;
    [SerializeField] float clipTimeSECONDS;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(clipTimeSECONDS);
        AudioManager.instance.PlayMusic(_clip);
        StartCoroutine(Musicloop());
    }
    IEnumerator Musicloop()
    {
        yield return new WaitForSeconds(clipTimeSECONDS);
        AudioManager.instance.PlayMusic(_clip);
        StartCoroutine(Musicloop());
    }
}