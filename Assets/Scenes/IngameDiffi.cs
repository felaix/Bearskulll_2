using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameDiffi : MonoBehaviour
{

    public GameObject DiffiEasy;
    public GameObject DiffiNormal;
    public GameObject DiffiHard;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveGame.Exists("Diffi"))
        {
            if (SaveGame.Load<string>("Diffi") == "Normal")
            {
                DiffiNormal.SetActive(true);
                DiffiEasy.SetActive(false);
                DiffiHard.SetActive(false);
            }
            if (SaveGame.Load<string>("Diffi") == "Easy")
            {
                DiffiNormal.SetActive(false);
                DiffiEasy.SetActive(true);
                DiffiHard.SetActive(false);
            }
            if (SaveGame.Load<string>("Diffi") == "Hard")
            {
                DiffiNormal.SetActive(false);
                DiffiEasy.SetActive(false);
                DiffiHard.SetActive(true);
            }



        }
        else
        {

            DiffiNormal.SetActive(true);
            DiffiEasy.SetActive(false);
            DiffiHard.SetActive(false);

        }
    }



}
