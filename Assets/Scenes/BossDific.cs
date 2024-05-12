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


            BossHealth._HP = 300;
            BossHealth._curHP = 300;
        }

      if(SaveGame.Load<string>("Diffi") == "Easy")
        {

            BossHealth._HP = 240;
            BossHealth._curHP = 240;

        }

      if(SaveGame.Load<string>("Diffi") == "Hard")
        {
            BossHealth._HP = 500;
            BossHealth._curHP = 500;

        }
     
}









}
