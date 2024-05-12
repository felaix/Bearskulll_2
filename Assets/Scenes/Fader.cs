using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Fader : MonoBehaviour
{
    // Also das ist alles nun aufm UI Canvas Loki Loki macht ob
    // Nun schon irgendwie fade ist

    public Image FadeImage;
    public float fadeDuration = 1.5f;

    public static Fader Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
            // Loki Loki if fade in machen

            if(SaveGame.Exists("FadeOut"))
            {
                FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, 1);

                StartCoroutine(FadeImageCoro(false));

                SaveGame.Delete("FadeOut");

            }
            else
            {
                FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, 0);
            FadeImage.gameObject.SetActive(false);

            }

        
    }




    public IEnumerator FadeImageCoro(bool fadeIn)
    {

        float alpha = fadeIn ? 0 : 1;
        float endAlpha = fadeIn ? 1 : 0;
        float fadeSpeed = 1 / fadeDuration;

        if(fadeIn)
        {
            SaveGame.Save<bool>("FadeOut", true);
            FadeImage.gameObject.SetActive(true);

        }

        while (Mathf.Abs(alpha - endAlpha) > 0.01f)
        {
            alpha = Mathf.MoveTowards(alpha, endAlpha, fadeSpeed * Time.deltaTime);
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, alpha);
            yield return null;
        }

        FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, endAlpha);

        if(!fadeIn)
        {
            FadeImage.gameObject.SetActive(false);

        }

    }




}
