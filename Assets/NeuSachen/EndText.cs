using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class EndText : MonoBehaviour
{
    public GameObject TextEN;
    public GameObject TextDE;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveGame.Load<bool>("SyncroEnd") == true)
        {
            SaveGame.Save<bool>("SyncroEnd", false);

            if (SaveGame.Load<string>("Language") == "German")
            {
                TextDE.SetActive(true);
            }
            else
            {
                TextEN.SetActive(true);
            }


        }


        if (!SaveGame.Exists("Souls")) 
        {
            SaveGame.Save<int>("Souls", 0);
        }




        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
