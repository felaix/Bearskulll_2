using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResetScript : MonoBehaviour
{

    public GameObject ResetDE;
    public GameObject ResetEN;



    public void OpenReset()
    {
        if( SaveGame.Load<string>("Language") == "German")
        {
            ResetDE.SetActive(true);
        }
        else
        {
            ResetEN.SetActive(true);
        }
    }


    public void Yes()
    {
        //SaveGame.DeleteAll();
        SaveGame.Delete("Souls");
        SaveGame.Delete("Level1");
        SaveGame.Delete("Level2");
        SaveGame.Delete("Level3");
        SaveGame.Delete("Level4");
        SaveGame.Delete("Level5");
        SaveGame.Delete("Level6");
        SaveGame.Delete("Level7");
        SaveGame.Delete("Level8");
        SaveGame.Delete("BigBag");
        SaveGame.Delete("SuperBag");
        SaveGame.Delete("HeroSword");
        SaveGame.Delete("KnightSword");
        SaveGame.Delete("BasicSword");
        SaveGame.Delete("MonsterDagger");
        SaveGame.Delete("BossAxe");
        SaveGame.Delete("Healpots");
        SaveGame.Delete("Energyy");
        SaveGame.Delete("Testes12d");
        SaveGame.Delete("WASDACTIVATED");
        SaveGame.Delete("Weapon");
        SaveGame.Delete("PlayerPosition");

        PlayerPrefs.DeleteAll();
        Application.Quit();
      //  SaveManager.instance.ResetSave();


        ResetDE.SetActive(false);
        ResetEN.SetActive(false);
    }

    public void No()
    {
        ResetDE.SetActive(false);
        ResetEN.SetActive(false);
    }


}
