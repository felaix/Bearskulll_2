using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDific : MonoBehaviour
{

    public Health BossHealth;



    private void Start()
    {
      
      if(SaveGame.Load<string>("Diffi") =="Normal")
        {


            BossHealth._HP = 400;
            BossHealth._curHP = 400;
            BossController.instance.ModeMultiplier = 1.5f;
        }

      if(SaveGame.Load<string>("Diffi") == "Easy")
        {

            BossHealth._HP = 300;
            BossHealth._curHP = 300;
            BossController.instance.ModeMultiplier = 1f;

        }

        if (SaveGame.Load<string>("Diffi") == "Hard")
        {
            BossHealth._HP = 500;
            BossHealth._curHP = 500;
            BossController.instance.ModeMultiplier = 1.8f;

        }

    }









}
