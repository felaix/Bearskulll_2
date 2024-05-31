using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class LanguageSwitch : MonoBehaviour
{

    [Header("DEUTSCH")] public GameObject[] LanguageObjects;
    [Header("ENGLISCH")] public GameObject[] LanguageObjectsOFF;

 //   public GameObject MenuEN;
    public GameObject MenuDE;


    // Start is called before the first frame update
    void Start()
    {
        if (!SaveGame.Exists("Language"))
        {
            SaveGame.Save("Language", "English");
        }
        
        UpdateLanguage();

    }

    public void German()
    {

        SaveGame.Save("Language", "German");
        UpdateLanguage();

    }

    public void English()
    {
        SaveGame.Save("Language", "English");
        UpdateLanguage();
    }

    public void UpdateLanguage()
    {
        if (SaveGame.Load<string>("Language") == "German")
        {
            foreach (GameObject LanguageObject in LanguageObjects)
            {
                LanguageObject.SetActive(true);
            }
            foreach (GameObject LanguageObjectOFF in LanguageObjectsOFF)
            {
                LanguageObjectOFF.SetActive(false);
            }
            
        }
        else
        {
            foreach (GameObject LanguageObject in LanguageObjects)
            {
                LanguageObject.SetActive(false);
            }
            foreach (GameObject LanguageObjectOFF in LanguageObjectsOFF)
            {
                LanguageObjectOFF.SetActive(true);
            }
        }

        MenuDE.SetActive(false);
      //  MenuEN.SetActive(false);
        
    }


    
    
}
