using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class SyncroEnd : MonoBehaviour
{

    public AudioClip[] _introClipsGerman;
    public AudioClip[] _introClipsEnglish;
    public AudioSource AS;
    public AudioSource Musicthing;
    float oldervolume;

    // Start is called before the first frame update
    void Start()
    {

        SaveGame.Save<bool>("SyncroEnd", true);

        if (SaveGame.Load<string>("Language") == "German")
        {
            StartCoroutine(PlayIntroGerman());
        }
        else
        {
            StartCoroutine(PlayIntroEnglish());
            Musicthing.volume = 0.083f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlayIntroEnglish()
    {
        yield return new WaitForSeconds(0.1f);

        float oldvolume = AudioListener.volume;
        oldervolume = oldvolume;

        Destroy(GameObject.Find("AudioManager"));

        AudioListener.volume = 1.0f;

        yield return new WaitForSeconds(0.1f);
        AS.PlayOneShot(_introClipsEnglish[0], 1.2f);
        yield return new WaitForSeconds(9.6f);
        AS.PlayOneShot(_introClipsEnglish[1], 1.2f);
        yield return new WaitForSeconds(7.6f);
        AS.PlayOneShot(_introClipsEnglish[2], 1.2f);
        yield return new WaitForSeconds(7.3f);
        AS.PlayOneShot(_introClipsEnglish[3], 1.2f);
        yield return new WaitForSeconds(6.3f);
        AS.PlayOneShot(_introClipsEnglish[4], 1.2f);
        yield return new WaitForSeconds(7.6f);
        AS.PlayOneShot(_introClipsEnglish[5], 1.2f);
        yield return new WaitForSeconds(8.3f);

    }

    IEnumerator PlayIntroGerman()
    {
        yield return new WaitForSeconds(0.1f);
        
        float oldvolume = AudioListener.volume;
        oldervolume = oldvolume;
        Destroy(GameObject.Find("AudioManager"));

        AudioListener.volume = 0.8f;
        AS.PlayOneShot(_introClipsGerman[0], 1.2f);
        yield return new WaitForSeconds(9.6f);
        AS.PlayOneShot(_introClipsGerman[1], 1.2f);
        yield return new WaitForSeconds(7.6f);
        AS.PlayOneShot(_introClipsGerman[2], 1.2f);
        yield return new WaitForSeconds(7.3f);
        AS.PlayOneShot(_introClipsGerman[3], 1.2f);
        yield return new WaitForSeconds(6.3f);
        AS.PlayOneShot(_introClipsGerman[4], 1.2f);
        yield return new WaitForSeconds(7.6f);
        AS.PlayOneShot(_introClipsGerman[5], 1.2f);
        yield return new WaitForSeconds(7.3f);


        











    }
    public void SkipIntro()
    {
        AudioListener.volume = oldervolume;
    }

}
