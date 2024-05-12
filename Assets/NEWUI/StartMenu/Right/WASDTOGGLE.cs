using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.UI;

public class WASDTOGGLE : MonoBehaviour
{
    bool WASDACTIVATED = false;

    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveGame.Exists("WASDACTIVATED"))
        {

            WASDACTIVATED = SaveGame.Load<bool>("WASDACTIVATED");
        
        
        }
        else
        {
            SaveGame.Save<bool>("WASDACTIVATED", false);
            WASDACTIVATED = false;
        }


        if (WASDACTIVATED)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }



    }

    public void ChangeWASD(bool Duhurensohn)
    {
          WASDACTIVATED = Duhurensohn;
        SaveGame.Save<bool>("WASDACTIVATED", WASDACTIVATED);
    }




}
